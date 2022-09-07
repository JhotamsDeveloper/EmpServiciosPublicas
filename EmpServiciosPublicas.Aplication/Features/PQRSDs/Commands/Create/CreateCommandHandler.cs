using AutoMapper;
using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicas.Aplication.Handlers;
using EmpServiciosPublicas.Aplication.Models;
using EmpServiciosPublicos.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCommand> _logger;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly StorageSetting _storageSetting;
        private string? message = null;

        public CreateCommandHandler(IMapper mapper, ILogger<CreateCommand> logger, IEmailService emailService, IUnitOfWork unitOfWork, IUploadFilesService uploadFilesService, StorageSetting storageSetting)
        {
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _storageSetting = storageSetting;
        }

        public async Task<string> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            string nameFile;
            string path;
            string[] formatsArray;

            bool validateFiles;
            bool validateFileSize;

            int responseComplete;
            int size;

            PQRSD pqrsdEntity;
            Storage storage;

            formatsArray = _storageSetting.DocumentsFormats.Split(',');
            validateFiles = request.Files!.ValidateCorrectFileFormat(formatsArray);
            if (!validateFiles)
            {
                _logger.LogError(message);
                throw new BadRequestException($"Los documentos debe de contener alguna de estas extensiones {string.Join(" ", formatsArray)}");
            }

            size = int.Parse(_storageSetting.Size);
            validateFileSize = request.Files!.ValidateFileSize(size);
            if (!validateFileSize)
            {
                _logger.LogError(message);
                throw new BadRequestException($"El tamaño de los documentos debe de contener máximo {size / 1048576} mb");
            }

            pqrsdEntity = _mapper.Map<PQRSD>(request);
            pqrsdEntity.Ref = pqrsdEntity.Type!.GenericReference();
            pqrsdEntity.Url = pqrsdEntity!.Title!.Create();
            pqrsdEntity.PQRSDStatus = "Create";

            _unitOfWork.PQRSDRepository.Add(pqrsdEntity);
            responseComplete = await _unitOfWork.Complete();
            if (responseComplete <= 0)
            {
                message = "No fue posible crear un PQRSD correctamente";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            foreach (IFormFile file in request.Files!)
            {
                (nameFile, path) = await _uploadFilesService.UploadedFileAsync(file, ProcessType.PQRSD.ToString(), Folder.Documents.ToString());
                storage = new()
                {
                    PqrsdId = pqrsdEntity.Id,
                    NameFile = nameFile,
                    RouteFile = path,
                    Rol = Folder.Documents.ToString(),
                    Availability = true
                };
                await _unitOfWork.Repository<Storage>().AddAsync(storage);
                await _unitOfWork.Complete();
            }

            return SerealizeExtension<PQRSD>
                .Serealize(pqrsdEntity);
        }
    }
}
