using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }
}
