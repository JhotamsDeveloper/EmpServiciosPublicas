using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Commands.Delete
{
    public class DeletePostCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
