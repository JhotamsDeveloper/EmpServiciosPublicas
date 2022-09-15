using MediatR;
using AutoMapper;
using EmpServiciosPublicos.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;

namespace EmpServiciosPublicas.Aplication.Features.Categories.Commands.Delete
{
    internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCategoryCommandHandler> _logger;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly IConfiguration _configuration;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteCategoryCommandHandler> logger, IUploadFilesService uploadFilesService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _uploadFilesService = uploadFilesService;
            _configuration = configuration;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            int responseComplete;

            Category categoryDelete;

            IReadOnlyList<Category> categoryOld;

            string message;
            categoryOld = await _unitOfWork.Repository<Category>().GetAsync(x => x.Id == request.Id, t => t.OrderByDescending(s => s.Id), "Storages", true);
            categoryDelete = categoryOld.FirstOrDefault()!;

            if (categoryDelete == null)
                throw new NotFoundException(nameof(Category), request.Id);

            if (categoryDelete.Storages.Any())
            {
                foreach (var file in categoryDelete.Storages)
                {
                    await _unitOfWork.Repository<Storage>().DeleteAsync(file);
                }
            }

            await _unitOfWork.Repository<Category>().DeleteAsync(categoryDelete);
            responseComplete = await _unitOfWork.Complete();

            if (categoryDelete.Storages.Any())
            {
                foreach (var file in categoryDelete.Storages)
                {
                    await _uploadFilesService.DeleteUploadAsync(file.NameFile, ProcessType.Category.ToString(), Folder.Icono.ToString());
                }
            }

            message = $"La categoria con id {request.Id} fue elimado exitosamente";
            _logger.LogInformation(message);

            return Unit.Value;
        }
    }
}
