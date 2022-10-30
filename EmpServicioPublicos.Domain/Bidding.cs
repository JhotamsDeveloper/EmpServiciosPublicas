using EmpServiciosPublicos.Domain.Common;

namespace EmpServiciosPublicos.Domain
{
    public class Bidding: BaseDomain
    {
        public Bidding()
        {
            Storages = new HashSet<Storage>();
            TenderProposals = new HashSet<TenderProposal>();
        }

        public DateTime StartOfTheCall { get; set; }
        public DateTime EndOfTheCall { get; set; }
        public ICollection<TenderProposal> TenderProposals { get; set; }

        public ICollection<Storage> Storages { get; set; }
    }
}
