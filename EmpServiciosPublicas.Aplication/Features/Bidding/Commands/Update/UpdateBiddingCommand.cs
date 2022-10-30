using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Update
{
    public class UpdateBiddingCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Descrption { get; set; } = string.Empty;
        public DateTime StartOfTheCall { get; set; } = DateTime.Now;
        public DateTime EndOfTheCall { get; set; }
        public ICollection<IFormFile>? Documents { get; set; } = default!;
        public ICollection<IFormFile>? Images { get; set; } = default!;
    }
}
