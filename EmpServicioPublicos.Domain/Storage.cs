using EmpServiciosPublicos.Domain.Common;

namespace EmpServiciosPublicos.Domain
{
    public class Storage: Audit
    {

        public string? NameFile { get; set; }
        public string? RouteFile { get; set; }
        public string? Rol { get; set; }

        public int PostId { get; set; }
        public virtual Post? Post { get; set; }

        public int BiddingId { get; set; }
        public virtual Bidding? Bidding { get; set; }

        public int TenderProposalId { get; set; }
        public virtual TenderProposal? TenderProposal { get; set; }

        public int PqrsdId { get; set; }
        public virtual PQRSD? PQRSD { get; set; }
    }
}
