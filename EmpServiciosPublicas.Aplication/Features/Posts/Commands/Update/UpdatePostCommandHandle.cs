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

namespace EmpServiciosPublicas.Aplication.Features.Posts.Commands.Update
{
    public class UpdatePostCommandHandle : IRequestHandler<UpdatePostCommand>
    {

        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePostCommandHandle> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly StorageSetting _storageSetting;

        public UpdatePostCommandHandle(IMapper mapper, ILogger<UpdatePostCommandHandle> logger, IUnitOfWork unitOfWork, IUploadFilesService uploadFilesService, IOptions<StorageSetting> storageSetting)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _storageSetting = storageSetting.Value;
        }

        async Task<Unit> IRequestHandler<UpdatePostCommand, Unit>.Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            string[] supportedFormats;
            int responseComplete;

            Post postEntity = new();
            IReadOnlyList<Category> category;
            IReadOnlyList<Post> post;

            post = await _unitOfWork.Repository<Post>().GetAsync(x => x.Id.Equals(request.Id), t => t.OrderByDescending(s => s.Id), "Storages", true);
            if (post == null)
                BadRequestError("No se encontro ningún post para actualizar");

            postEntity = post!.FirstOrDefault()!;

            await DeleteFilesOld(x => x.Rol == Folder.Documents.ToString(), postEntity.Storages, Folder.Documents.ToString());
            await DeleteFilesOld(x => x.Rol == Folder.Image.ToString(), postEntity.Storages, Folder.Image.ToString());

            category = await _unitOfWork.Repository<Category>().GetAsync(x => x.Id.Equals(request.CategoryId));
            if (category.Count == 0)
                BadRequestError("No se encontro ningún id para la categoría");

            _mapper.Map(request, postEntity, typeof(UpdatePostCommand), typeof(Post));
            await _unitOfWork.Repository<Post>().UpdateAsync(postEntity);
            responseComplete = await _unitOfWork.Complete();
            if (responseComplete <= 0)
                BadRequestError("No fue posible actualizar el post correctamente");

            supportedFormats = _storageSetting.ImagesFormats.Split(',');
            await SaveFiles(request.Images!, supportedFormats, Folder.Image.ToString(), "imagen", postEntity.Id);

            supportedFormats = _storageSetting.DocumentsFormats.Split(',');
            await SaveFiles(request.Documents!, supportedFormats, Folder.Documents.ToString(), "documento", postEntity.Id);

            return Unit.Value;
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

        private async Task DeleteFilesOld(Func<Storage, bool> filter, ICollection<Storage> files, string folder)
        {
            if (files.Any())
            {
                foreach (var file in files.Where(filter))
                {
                    await _uploadFilesService.DeleteUploadAsync(file.NameFile, ProcessType.Post.ToString(), folder);
                    await _unitOfWork.Repository<Storage>().DeleteAsync(file);
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
