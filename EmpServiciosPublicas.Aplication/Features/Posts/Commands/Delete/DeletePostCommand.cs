using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Commands.Delete
{
    public class DeletePostCommand : IRequest
    {
        public int Id { get; set; }
    }
}
