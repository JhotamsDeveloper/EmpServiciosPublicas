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

namespace EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Update
{
    public class UpdateBiddingCommandHandler : IRequestHandler<UpdateBiddingCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBiddingCommandHandler> _logger;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly StorageSetting _storageSetting;

        public UpdateBiddingCommandHandler(IMapper mapper, ILogger<UpdateBiddingCommandHandler> logger, IUnitOfWork unitOfWork, IUploadFilesService uploadFilesService, IOptions<StorageSetting> storageSetting)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _storageSetting = storageSetting.Value;
        }

        public async Task<Unit> Handle(UpdateBiddingCommand request, CancellationToken cancellationToken)
        {
            string nameFile;
            string path;
            string[] formatsArray;
            string message = string.Empty;

            bool validateFiles;
            bool validateFileSize;

            int responseComplete;
            long size;
            long tickes = DateTime.Now.Ticks;

            IReadOnlyList<BiddingEntity> biddingOld;

            List<IFormFile> documentsAndImages = new();
            documentsAndImages.AddRange(request!.Documents!);

            if (request.Images != null && request.Images.Any())
                documentsAndImages.AddRange(request!.Images!);

            BiddingEntity biddingEntity = new();
            Storage storage;

            formatsArray = _storageSetting.DocumentsFormats.Split(',');
            validateFiles = request.Documents!.ValidateCorrectFileFormat(formatsArray);
            if (!validateFiles)
            {
                _logger.LogError(message);
                throw new BadRequestException($"Los documentos debe de contener alguna de estas extensiones {string.Join(" ", formatsArray)}");
            }

            size = long.Parse(_storageSetting.Size);
            validateFileSize = documentsAndImages.ValidateFileSize(size);
            if (!validateFileSize)
            {
                _logger.LogError(message);
                throw new BadRequestException($"El tamaño de los documentos y/o las imagenes debe de contener máximo {size / 1048576} mb");
            }

            biddingOld = await _unitOfWork.Repository<BiddingEntity>()
                .GetAsync(x => x.Id == request.Id, t => t.OrderByDescending(s => s.Id), nameof(BiddingEntity), true);

            biddingEntity = biddingOld.FirstOrDefault()!;

            if (biddingOld == null)
                throw new NotFoundException(nameof(BiddingEntity), request.Id);

            if (biddingEntity.Storages.Any())
            {
                foreach (var file in biddingEntity.Storages)
                {
                    await _uploadFilesService.DeleteUploadAsync(file.NameFile, ProcessType.Bidding.ToString(), Folder.Documents.ToString());
                    await _uploadFilesService.DeleteUploadAsync(file.NameFile, ProcessType.Bidding.ToString(), Folder.Image.ToString());
                    await _unitOfWork.Repository<Storage>().DeleteAsync(file);
                    await _unitOfWork.Complete();
                }
            }

            _mapper.Map(request, biddingEntity, typeof(UpdateBiddingCommandHandler), typeof(BiddingEntity));
            await _unitOfWork.Repository<BiddingEntity>().UpdateAsync(biddingEntity);
            responseComplete = await _unitOfWork.Complete();

            if (responseComplete <= 0)
            {
                message = "No fue posible crear una convocatoria correctamente";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            foreach (IFormFile file in request!.Documents!)
            {
                (nameFile, path) = await _uploadFilesService.UploadedFileAsync(file, ProcessType.Bidding.ToString(), Folder.Documents.ToString());
                storage = new()
                {
                    BiddingId = request.Id,
                    NameFile = nameFile,
                    RouteFile = path,
                    Rol = Folder.Documents.ToString(),
                    Availability = true
                };
                await _unitOfWork.Repository<Storage>().AddAsync(storage);
                var idStorege = await _unitOfWork.Complete();
            }

            foreach (IFormFile file in request!.Images!)
            {
                (nameFile, path) = await _uploadFilesService.UploadedFileAsync(file, ProcessType.Bidding.ToString(), Folder.Image.ToString());
                storage = new()
                {
                    BiddingId = request.Id,
                    NameFile = nameFile,
                    RouteFile = path,
                    Rol = Folder.Documents.ToString(),
                    Availability = true
                };
                await _unitOfWork.Repository<Storage>().AddAsync(storage);
                var idStorege = await _unitOfWork.Complete();
            }

            return Unit.Value;
        }
    }
}
