using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Commands.Update
{
    public class UpdatePostCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; } = default!;
        public string Descrption { get; set; } = default!;
        public bool Availability { get; set; }
        public ICollection<IFormFile>? Documents { get; set; } = default!;
        public ICollection<IFormFile>? Images { get; set; } = default!;
    }
}
