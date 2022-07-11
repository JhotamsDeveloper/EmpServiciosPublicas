using EmpServiciosPublicos.Domain.Common;

namespace EmpServiciosPublicos.Domain
{
    public class Bidding: BaseDomain
    {
        public DateTime StartOfTheCall { get; set; }
        public DateTime EndOfTheCall { get; set; }
    }
}
