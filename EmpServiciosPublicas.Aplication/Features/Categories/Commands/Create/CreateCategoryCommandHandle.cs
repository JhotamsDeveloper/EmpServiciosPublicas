using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Create;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Handlers;
using EmpServiciosPublicas.Aplication.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EmpServiciosPublicos.Domain;
using AutoMapper;
using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandle : IRequestHandler<CreateCategoryCommand, string>
    {

        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandle> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly StorageSetting _storageSetting;
        private string? message = null;

        public CreateCategoryCommandHandle(IMapper mapper, ILogger<CreateCategoryCommandHandle> logger, IUnitOfWork unitOfWork, IUploadFilesService uploadFilesService, IOptions<StorageSetting> storageSetting)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _storageSetting = storageSetting.Value;
        }

        public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            string nameFile;
            string path;
            string[] formatsArray;

            bool validateFiles;
            bool validateFileSize;

            int responseComplete;
            int size;

            Category category;
            Storage storage;
            IReadOnlyList<Category> searchByCategory;

            searchByCategory = await _unitOfWork.Repository<Category>().GetAsync(x => x.Title!.ToUpper() == request.Title.ToUpper());
            if (searchByCategory != null)
                throw new BadRequestException($"Ya existe una categoria {request.Title.ToLower()} con el id {searchByCategory[0].Id}");

            formatsArray = _storageSetting.ImagesFormats.Split(',');
            validateFiles = request.Icono!.ValidateCorrectFileFormat(formatsArray);
            if (!validateFiles)
            {
                _logger.LogError(message);
                throw new BadRequestException($"El icono debe de contener alguna de estas extensiones {string.Join(" ", formatsArray)}");
            }

            size = int.Parse(_storageSetting.IconSize);
            validateFileSize = request.Icono!.ValidateFileSize(size);
            if (!validateFileSize)
            {
                _logger.LogError(message);
                throw new BadRequestException($"El tamaño del icono debe de contener máximo {size / 1048576} mb");
            }

            category = _mapper.Map<Category>(request);
            category.Url = request!.Title!.Create();

            _unitOfWork.Repository<Category>().Add(category);
            responseComplete = await _unitOfWork.Complete();
            if (responseComplete <= 0)
            {
                message = "No fue posible crear una categoria correctamente";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            (nameFile, path) = await _uploadFilesService.UploadedFileAsync(request.Icono, ProcessType.Category.ToString(), Folder.Icono.ToString());
            storage = new()
            {
                CategoryId = category.Id,
                NameFile = nameFile,
                RouteFile = path,
                Rol = Folder.Icono.ToString(),
                Availability = true
            };
            await _unitOfWork.Repository<Storage>().AddAsync(storage);
            responseComplete = await _unitOfWork.Complete();
            if (responseComplete <= 0)
            {
                message = "No fue posible crear una categoria correctamente";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            return SerealizeExtension<Category>.Serealize(category);
        }
    }
}
