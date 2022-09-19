using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Commands.Update
{
    public class UpdatePostCommand : IRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = default!;
        public string Descrption { get; set; } = default!;
        public ICollection<IFormFile>? Documents { get; set; } = default!;
        public ICollection<IFormFile>? Images { get; set; } = default!;
    }
}
