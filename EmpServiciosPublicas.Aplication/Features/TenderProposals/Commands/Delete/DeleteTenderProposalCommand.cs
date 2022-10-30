using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Delete
{
    public class DeleteTenderProposalCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
