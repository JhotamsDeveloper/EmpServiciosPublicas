using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicos.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Commands.Delete
{
    public class DeletePostCommandHandle : IRequestHandler<DeletePostCommand>
    {
        private readonly IUploadFilesService _uploadFilesService;
        private readonly ILogger<DeletePostCommand> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePostCommandHandle(ILogger<DeletePostCommand> logger, IUnitOfWork unitOfWork, IUploadFilesService uploadFilesService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            string message;
            int responseComplete;
            Post postEntity = new();
            IReadOnlyList<Post> post;

            post = await _unitOfWork.Repository<Post>().GetAsync(x => x.Id.Equals(request.Id), t => t.OrderByDescending(s => s.Id), "Storages", true);
            if (post == null)
                BadRequestError("No se encontro ningún post para eliminarse");

            postEntity = post!.FirstOrDefault()!;

            await _unitOfWork.Repository<Post>().DeleteAsync(postEntity);
            responseComplete = await _unitOfWork.Complete();
            if (responseComplete <= 0)
                BadRequestError("No fue posible eliminar el post correctamente");

            await DeleteFilesOld(x => x.Rol == Folder.Documents.ToString(), postEntity.Storages, Folder.Documents.ToString());
            await DeleteFilesOld(x => x.Rol == Folder.Image.ToString(), postEntity.Storages, Folder.Image.ToString());

            message = $"El post con id {request.Id} fue elimado exitosamente";
            _logger.LogInformation(message);

            return Unit.Value;
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
