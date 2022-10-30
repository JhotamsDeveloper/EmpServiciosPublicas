using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Create
{
    public class CreateTenderProposalCommand : IRequest<string>
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Surnames { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentNumer { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }
        public Guid BiddingId { get; set; }
        public IFormFile File { get; set; } = default!;
    }
}
