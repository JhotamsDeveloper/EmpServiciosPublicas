using AutoMapper;
using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicas.Aplication.Features.Categories.Commands.Create;
using EmpServiciosPublicas.Aplication.Handlers;
using EmpServiciosPublicas.Aplication.Models;
using EmpServiciosPublicos.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;

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

            categoryOld = await _unitOfWork.Repository<Category>().GetAsync(x => x.Id == request.Id, t => t.OrderByDescending(s => s.Id), string.Empty, true);

            if (categoryOld == null)
                throw new NotFoundException(nameof(PQRSD), request.Id);

            var existCategory = categoryOld.All(c => c.Title!.ToUpper() == request.Title.ToUpper());
            if (existCategory)
            {
                _logger.LogError(message);
                throw new BadRequestException($"Ya existe una categoria llamada {request.Title.ToLower()}");
            }

            category = categoryOld.FirstOrDefault()!;
            await _uploadFilesService.DeleteUploadAsync(category.NameIcono!, ProcessType.Category.ToString(), Folder.Icono.ToString());

            (nameFile, path) = await _uploadFilesService.UploadedFileAsync(request.Icono, ProcessType.Category.ToString(), Folder.Icono.ToString());
            _mapper.Map(request, category, typeof(UpdateCategoryCommand), typeof(Category));
            category.Url = request!.Title!.Create();
            category.RouteIcono = path;
            category.NameIcono = nameFile;

            _unitOfWork.Repository<Category>().Update(category);
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