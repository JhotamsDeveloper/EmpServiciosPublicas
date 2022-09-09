using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous
{
    public class CreateAnonymousCommand : IRequest<string>
    {
        public string Title { get; set; } = string.Empty;
        public string Descrption { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public ICollection<IFormFile>? Files { get; set; }
    }
}
