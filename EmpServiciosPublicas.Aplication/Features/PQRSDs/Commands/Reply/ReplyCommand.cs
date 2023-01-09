using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Reply
{
    public class ReplyCommand : IRequest<string>
    {
        public Guid IdPqrsd { get; set; }
        public string Reply { get; set; } = default!;
        public IFormFile? File { get; set; }
    }
}
