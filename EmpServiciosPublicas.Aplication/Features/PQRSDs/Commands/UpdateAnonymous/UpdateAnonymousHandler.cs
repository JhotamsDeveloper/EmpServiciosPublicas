using AutoMapper;
using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicas.Aplication.Handlers;
using EmpServiciosPublicos.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.UpdateAnonymous
{
    public class UpdateAnonymousHandler : IRequestHandler<UpdateAnonymousCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAnonymousHandler> _logger;
        private readonly IEmailService _emailService;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly IConfiguration _configuration;
        private string? message = null;

        public UpdateAnonymousHandler(IMapper mapper, ILogger<UpdateAnonymousHandler> logger, IEmailService emailService, IUnitOfWork unitOfWork, IUploadFilesService uploadFilesService, IConfiguration configuration)
        {
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _configuration = configuration;
        }

        public async Task<Unit> Handle(UpdateAnonymousCommand request, CancellationToken cancellationToken)
        {

            string formats;
            string nameFile;
            string path;
            string[] formatsArray;

            bool validateFiles;
            bool validateFileSize;

            int responseComplete;
            long size;
            long tickes = DateTime.Now.Ticks;

            PQRSD pqrsdUpdate;

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

            formats = _configuration.GetSection("Storage:DocumentsFormats").Value;
            formatsArray = formats.Split(',');
            validateFiles = request.Files.ValidateCorrectFileFormat(formatsArray);
            if (!validateFiles)
            {
                _logger.LogError(message);
                throw new BadRequestException($"Los documentos debe de contener alguna de estas extensiones {string.Join(" ", formatsArray)}");
            }

            size = long.Parse(_configuration.GetSection("Storage:Size").Value);
            validateFileSize = request.Files.ValidateFileSize(size);
            if (!validateFileSize)
            {
                _logger.LogError(message);
                throw new BadRequestException($"El tamaño de los documentos debe de contener máximo {size / 1048576} mb");
            }

            var pqrsdOld = await _unitOfWork.Repository<PQRSD>().GetAsync(x => x.Id == request.Id, null, "Storages", true);
            pqrsdUpdate = pqrsdOld.FirstOrDefault()!;

            if (pqrsdUpdate == null)
                throw new NotFoundException(nameof(PQRSD), request.Id);


            if (pqrsdUpdate.Storages.Any())
            {
                foreach (var file in pqrsdUpdate.Storages)
                {
                    await _uploadFilesService.DeleteUploadAsync(file.NameFile, ProcessType.PQRSD.ToString(), Folder.Documents.ToString());
                    await _unitOfWork.Repository<Storage>().DeleteAsync(file);
                    await _unitOfWork.Complete();
                }
            }

            //Otra forma de mappear
            _mapper.Map(request, pqrsdUpdate, typeof(UpdateAnonymousCommand), typeof(PQRSD));

            pqrsdUpdate.Url = pqrsdUpdate!.Title!.Create();
            await _unitOfWork.Repository<PQRSD>().UpdateAsync(pqrsdUpdate);
            responseComplete = await _unitOfWork.Complete();

            if (responseComplete <= 0)
            {
                message = "No fue posible crear un PQRSD correctamente";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            foreach (IFormFile file in request.Files)
            {
                (nameFile, path) = await _uploadFilesService.UploadedFileAsync(file, ProcessType.PQRSD.ToString(), Folder.Documents.ToString());
                Storage storage = new()
                {
                    PqrsdId = request.Id,
                    NameFile = nameFile,
                    RouteFile = path,
                    Rol = Folder.Documents.ToString(),
                    Availability = true
                };
                await _unitOfWork.Repository<Storage>().AddAsync(storage);
                var idStorege = await _unitOfWork.Complete();
            }

            //Envios de correo electrónico
            //....

            return Unit.Value;
        }
    }
}
