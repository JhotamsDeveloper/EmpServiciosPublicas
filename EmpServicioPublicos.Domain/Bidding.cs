using EmpServiciosPublicos.Domain.Common;

namespace EmpServiciosPublicos.Domain
{
    public class Bidding: BaseDomain
    {
        public Bidding()
        {
            Storages = new HashSet<Storage>();
        }

        public DateTime StartOfTheCall { get; set; }
        public DateTime EndOfTheCall { get; set; }

        public ICollection<Storage> Storages { get; set; }
    }
}
