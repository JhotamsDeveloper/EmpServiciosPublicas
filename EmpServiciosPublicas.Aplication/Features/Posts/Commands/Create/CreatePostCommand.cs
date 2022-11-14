using EmpServiciosPublicas.Aplication.Features.Posts.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Commands.Create
{
    public class CreatePostCommand : IRequest<PostResponse>
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; } = default!;
        public string Descrption { get; set; } = default!;
        public ICollection<IFormFile>? Documents { get; set; } = default!;
        public ICollection<IFormFile>? Images { get; set; } = default!;

    }
}
