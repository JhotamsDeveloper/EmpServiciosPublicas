using AutoMapper;
using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicas.Aplication.Features.Posts.Models;
using EmpServiciosPublicas.Aplication.Handlers;
using EmpServiciosPublicas.Aplication.Models;
using EmpServiciosPublicos.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Commands.Create
{
    public class CreatePostCommandHandle : IRequestHandler<CreatePostCommand, PostResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePostCommandHandle> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly StorageSetting _storageSetting;

        public CreatePostCommandHandle(IMapper mapper, ILogger<CreatePostCommandHandle> logger, IUnitOfWork unitOfWork, IUploadFilesService uploadFilesService, IOptions<StorageSetting> storageSetting)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _storageSetting = storageSetting.Value;
        }

        public async Task<PostResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            string[] supportedFormats;
            int responseComplete;

            Post postEntity = new();
            PostResponse response = new();

            await ValidateCategory(request);

            _mapper.Map(request, postEntity, typeof(CreatePostCommand), typeof(Post));
            postEntity.Url = request!.Title!.Create();

            await _unitOfWork.Repository<Post>().AddAsync(postEntity);
            responseComplete = await _unitOfWork.Complete();
            if (responseComplete <= 0)
                BadRequestError("No fue posible crear el post correctamente");

            supportedFormats = _storageSetting.ImagesFormats.Split(',');
            await SaveFiles(request.Images!, supportedFormats, Folder.Image.ToString(), "imagen", postEntity.Id);

            supportedFormats = _storageSetting.DocumentsFormats.Split(',');
            await SaveFiles(request.Documents!, supportedFormats, Folder.Documents.ToString(), "documento", postEntity.Id);

            _mapper.Map(postEntity, response, typeof(Post), typeof(PostResponse));
            return response;
        }

        private async Task ValidateCategory(CreatePostCommand request)
        {
            var category = await _unitOfWork.Repository<Category>().GetAsync(x => x.Id.Equals(request.CategoryId));
            if (category.Count == 0)
                BadRequestError("No se encontro ningún id para la categoría");
        }

        private async Task SaveFiles(ICollection<IFormFile> files, string[] supportedFormats, string folder, string folderDescription, Guid postEntityId)
        {
            Storage storage;
            string nameFile;
            string path;

            bool validateFiles;
            bool validateFileSize;

            int size;

            if (files != null && files.Any())
            {
                validateFiles = files!.ValidateCorrectFileFormat(supportedFormats);
                if (!validateFiles)
                    BadRequestError($"La extensión de cada {folderDescription} debe de contener alguna de estas extensiones {string.Join(" ", supportedFormats)}");

                size = int.Parse(_storageSetting.Size);
                validateFileSize = files!.ValidateFileSize(size);
                if (!validateFileSize)
                    BadRequestError($"El tamaño de cada {folderDescription} debe ser máximo de {size / 1048576} mb");

                foreach (IFormFile file in files)
                {
                    (nameFile, path) = await _uploadFilesService.UploadedFileAsync(file, ProcessType.Post.ToString(), folder);
                    storage = new()
                    {
                        PostId = postEntityId,
                        NameFile = nameFile,
                        RouteFile = path,
                        Rol = folder,
                        Availability = true
                    };
                    await _unitOfWork.Repository<Storage>().AddAsync(storage);
                    await _unitOfWork.Complete();
                }
            }
        }

        private void BadRequestError(string msg)
        {
            _logger.LogError(msg);
            throw new BadRequestException(msg);
        }
    }

}
