using AutoMapper;
using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicos.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous
{
    public class CreateAnonymousCommandHandler : IRequestHandler<CreateAnonymousCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAnonymousCommandHandler> _logger;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly IConfiguration _configuration;
        private string? message = null;

        public CreateAnonymousCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateAnonymousCommandHandler>
            logger, IEmailService emailService, IUploadFilesService uploadFilesService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
            _uploadFilesService = uploadFilesService;
            _configuration = configuration;
        }

        public async Task<string> Handle(CreateAnonymousCommand request, CancellationToken cancellationToken)
        {
            long tickes = DateTime.Now.Ticks;
            PQRSD pqrsdEntity = _mapper.Map<PQRSD>(request);

            if (request.Files == null)
            {
                message = "Es necesario adjuntar un documento relacionado a su petición";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            if (!request.Files.Any())
            {
                message = "Es necesario adjuntar un documento relacionado a su petición";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            string formats = _configuration.GetSection("Storage:DocumentsFormats").Value;
            string[] formatsArray = formats.Split(',');
            bool validateFiles = ValidateCorrectFileFormat(request.Files, formatsArray);
            if (!validateFiles)
            {
                _logger.LogError(message);
                throw new BadRequestException($"Los documentos debe de contener alguna de estas extensiones {string.Join(" ", formatsArray)}");
            }

            long size = long.Parse(_configuration.GetSection("Storage:Size").Value);
            bool validateFileSize = ValidateFileSize(request.Files, size);
            if (!validateFileSize)
            {
                _logger.LogError(message);
                throw new BadRequestException($"El tamaño de los documentos debe de contener máximo {size / 1048576} mb");
            }

            pqrsdEntity.Ref = $"{pqrsdEntity.PQRSDType}_{tickes:D20}";
            pqrsdEntity.Url = string.Join("-", pqrsdEntity.Title!.Split('@', ',', '.', ';', '\'', ' ')).ToLower();
            pqrsdEntity.PQRSDStatus = "Create";

            _unitOfWork.PQRSDRepository.AddEntity(pqrsdEntity);
            int idpqrsd = await _unitOfWork.Complete();

            if (idpqrsd <= 0)
            {
                message = "No fue posible crear un PQRSD correctamente";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }


            foreach (var file in request.Files)
            {
                var (nameFile, path) = await _uploadFilesService.UploadedFileAsync(file, ProcessType.PQRSD.ToString(), Folder.Documents.ToString());
                Storage storage = new()
                {
                    PqrsdId = idpqrsd,
                    NameFile = nameFile,
                    RouteFile = path,
                    Rol = Folder.Documents.ToString(),
                    Availability = true
                };
                await _unitOfWork.Repository<Storage>().AddAsync(storage);
            }

            //Envios de correo electrónico
            //....

            return pqrsdEntity.Ref;
        }


        private static bool ValidateCorrectFileFormat(ICollection<IFormFile> files, string[] permittedExtensions)
        {
            foreach (var file in files)
            {
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!(!string.IsNullOrEmpty(ext) && permittedExtensions.Contains(ext)))
                    return false;
            }
            return true;
        }

        private static bool ValidateFileSize(ICollection<IFormFile> files, long size)
        {
            foreach (var file in files)
            {
                if (file.Length > size)
                    return false;
            }
            return true;
        }
    }
}
