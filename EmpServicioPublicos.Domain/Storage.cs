using EmpServiciosPublicos.Domain.Common;

namespace EmpServiciosPublicos.Domain
{
    public class Storage: Audit
    {

        public string NameFile { get; set; } = string.Empty;
        public string RouteFile { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;

        public Guid? PostId { get; set; }
        public virtual Post? Post { get; set; }

        public Guid? BiddingId { get; set; }
        public virtual Bidding? Bidding { get; set; }

        public Guid? TenderProposalId { get; set; }
        public virtual TenderProposal? TenderProposal { get; set; }

        public Guid? PqrsdId { get; set; }
        public virtual PQRSD? PQRSD { get; set; }

        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
