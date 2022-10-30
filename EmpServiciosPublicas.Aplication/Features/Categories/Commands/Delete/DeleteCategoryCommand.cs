using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
