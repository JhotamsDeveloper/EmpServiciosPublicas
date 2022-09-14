using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Features.Categories.Commands.Create
{
    public class UpdateCategoryCommand: IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Descrption { get; set; } = default!;
        public IFormFile Icono { get; set; } = default!;
    }
}