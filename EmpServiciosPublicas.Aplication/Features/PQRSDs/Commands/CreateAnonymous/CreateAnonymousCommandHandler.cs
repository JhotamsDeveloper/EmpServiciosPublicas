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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        private readonly StorageSetting _storageSetting;
        private string? message = null;

        public CreateAnonymousCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateAnonymousCommandHandler>
            logger, IEmailService emailService, IUploadFilesService uploadFilesService, IConfiguration configuration, IOptions<StorageSetting> storageSetting)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
            _uploadFilesService = uploadFilesService;
            _configuration = configuration;
            _storageSetting = storageSetting.Value;
        }

        public async Task<string> Handle(CreateAnonymousCommand request, CancellationToken cancellationToken)
        {
            string nameFile;
            string path;
            string[] formatsArray;

            bool validateFiles;
            bool validateFileSize;

            int responseComplete;
            long size;

            PQRSD pqrsdEntity;
            Storage storage;

            formatsArray = _storageSetting.DocumentsFormats.Split(',');
            validateFiles = request.Files!.ValidateCorrectFileFormat(formatsArray);
            if (!validateFiles)
            {
                _logger.LogError(message);
                throw new BadRequestException($"Los documentos debe de contener alguna de estas extensiones {string.Join(" ", formatsArray)}");
            }

            size = long.Parse(_storageSetting.Size);
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





            //Envios de correo electrónico
            //....

            return JsonSerializer.Serialize(pqrsdEntity, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
        }
    }
}
