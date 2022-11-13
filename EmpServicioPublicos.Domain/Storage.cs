using EmpServiciosPublicos.Domain.Common;

namespace EmpServiciosPublicos.Domain
{
    public class Storage: BaseDomain
    {
        public string NameFile { get; set; } = string.Empty;
        public string RouteFile { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public Guid? PostId { get; set; }
        public Guid? BiddingId { get; set; }
        public Guid? TenderProposalId { get; set; }
        public Guid? PqrsdId { get; set; }
    }
}
