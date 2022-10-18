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
using Microsoft.Extensions.Options;
using BiddingEntity = EmpServiciosPublicos.Domain.Bidding;

namespace EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Create
{
    public class CreateBiddingCommandHandler : IRequestHandler<CreateBiddingCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBiddingCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly StorageSetting _storageSetting;
        private string? message = null;


        public CreateBiddingCommandHandler(IMapper mapper, ILogger<CreateBiddingCommandHandler> logger, IUnitOfWork unitOfWork, 
            IUploadFilesService uploadFilesService, IOptions<StorageSetting> storageSetting)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _storageSetting = storageSetting.Value;
        }

        public async Task<string> Handle(CreateBiddingCommand request, CancellationToken cancellationToken)
        {
            string nameFile;
            string path;
            string[] documentFormat;
            string[] imagesFormat;

            bool validateFiles;
            bool validateFileSize;

            int responseComplete;
            int size;

            BiddingEntity biddingEntity = new();
            Storage storage;

            _mapper.Map(request, biddingEntity, typeof(CreateBiddingCommand), typeof(BiddingEntity));
            biddingEntity.Url = request!.Title!.Create();

            await _unitOfWork.Repository<BiddingEntity>().AddAsync(biddingEntity);
            responseComplete = await _unitOfWork.Complete();
            if (responseComplete <= 0)
            {
                message = "No fue posible crear el licitación correctamente";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            if (request.Images != null && request.Images.Any())
            {
                imagesFormat = _storageSetting.ImagesFormats.Split(',');
                validateFiles = request.Images!.ValidateCorrectFileFormat(imagesFormat);
                if (!validateFiles)
                {
                    _logger.LogError(message);
                    throw new BadRequestException($"Las imagenes debe de contener alguna de estas extensiones {string.Join(" ", imagesFormat)}");
                }

                size = int.Parse(_storageSetting.Size);
                validateFileSize = request.Images!.ValidateFileSize(size);
                if (!validateFileSize)
                {
                    _logger.LogError(message);
                    throw new BadRequestException($"El tamaño de cada imagen debe ser máximo de {size / 1048576} mb");
                }

                foreach (IFormFile file in request.Images)
                {
                    (nameFile, path) = await _uploadFilesService.UploadedFileAsync(file, ProcessType.Bidding.ToString(), Folder.Image.ToString());
                    storage = new()
                    {
                        BiddingId = biddingEntity.Id,
                        NameFile = nameFile,
                        RouteFile = path,
                        Rol = Folder.Image.ToString(),
                        Availability = true
                    };
                    await _unitOfWork.Repository<Storage>().AddAsync(storage);
                    await _unitOfWork.Complete();
                }
            }


            if (request.Documents != null && request.Documents.Any())
            {
                documentFormat = _storageSetting.DocumentsFormats.Split(',');
                validateFiles = request.Documents!.ValidateCorrectFileFormat(documentFormat);
                if (!validateFiles)
                {
                    _logger.LogError(message);
                    throw new BadRequestException($"Los documentos debe de contener alguna de estas extensiones {string.Join(" ", documentFormat)}");
                }

                size = int.Parse(_storageSetting.Size);
                validateFileSize = request.Documents!.ValidateFileSize(size);
                if (!validateFileSize)
                {
                    _logger.LogError(message);
                    throw new BadRequestException($"El tamaño de cada documento debe ser máximo de {size / 1048576} mb");
                }

                foreach (IFormFile file in request.Documents)
                {
                    (nameFile, path) = await _uploadFilesService.UploadedFileAsync(file, ProcessType.Bidding.ToString(), Folder.Documents.ToString());
                    storage = new()
                    {
                        BiddingId = biddingEntity.Id,
                        NameFile = nameFile,
                        RouteFile = path,
                        Rol = Folder.Documents.ToString(),
                        Availability = true
                    };
                    await _unitOfWork.Repository<Storage>().AddAsync(storage);
                    await _unitOfWork.Complete();
                }
            }

            return SerealizeExtension<BiddingEntity>
                .Serealize(biddingEntity);
        }
    }
}
