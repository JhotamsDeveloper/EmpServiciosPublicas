using EmpServiciosPublicas.Aplication.Features.Categories.Commands.Create;
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

namespace EmpServiciosPublicas.Aplication.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandle : IRequestHandler<UpdateCategoryCommand>
    {

        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandle> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly StorageSetting _storageSetting;
        private string? message = null;

        public UpdateCategoryCommandHandle(IMapper mapper, ILogger<CreateCategoryCommandHandle> logger, IUnitOfWork unitOfWork, IUploadFilesService uploadFilesService, IOptions<StorageSetting> storageSetting)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _storageSetting = storageSetting.Value;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
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
            IReadOnlyList<Category> categoryOld;

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

            categoryOld = await _unitOfWork.Repository<Category>().GetAsync(x => x.Id == request.Id, t => t.OrderByDescending(s => s.Id), "Storages", true);
            
            if (categoryOld == null)
                throw new NotFoundException(nameof(PQRSD), request.Id);

            category = categoryOld.FirstOrDefault()!;
            if (category.Storages.Any())
            {
                foreach (var file in category.Storages)
                {
                    await _uploadFilesService.DeleteUploadAsync(file.NameFile, ProcessType.PQRSD.ToString(), Folder.Documents.ToString());
                    await _unitOfWork.Repository<Storage>().DeleteAsync(file);
                    await _unitOfWork.Complete();
                }
            }

            _mapper.Map(request, category, typeof(UpdateCategoryCommand), typeof(Category));
            category.Url = request!.Title!.Create();

            _unitOfWork.Repository<Category>().Update(category);
            responseComplete = await _unitOfWork.Complete();
            if (responseComplete <= 0)
            {
                message = "No fue posible actualizar una categoria correctamente";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            (nameFile, path) = await _uploadFilesService.UploadedFileAsync(request.Icono, ProcessType.Category.ToString(), Folder.Icono.ToString());
            storage = new()
            {
                NameFile = nameFile,
                RouteFile = path,
                Rol = Folder.Icono.ToString(),
                Availability = true
            };
            await _unitOfWork.Repository<Storage>().AddAsync(storage);
            responseComplete = await _unitOfWork.Complete();
            if (responseComplete <= 0)
            {
                message = "No fue posible actualizar una categoria correctamente";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            return Unit.Value;
        }
    }
}