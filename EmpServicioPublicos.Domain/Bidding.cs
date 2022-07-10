using EmpServicioPublicos.Domain.Common;

namespace EmpServicioPublicos.Domain
{
    public class Bidding: BaseDomain
    {
        public DateTime StartOfTheCall { get; set; }
        public DateTime EndOfTheCall { get; set; }
    }
}
