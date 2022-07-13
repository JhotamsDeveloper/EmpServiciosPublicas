using EmpServiciosPublicos.Domain.Common;

namespace EmpServiciosPublicos.Domain
{
    public class TenderProposal: Audit
    {
        public TenderProposal()
        {
            Storages = new HashSet<Storage>();
        }

        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Surnames { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentNumer { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }

        public ICollection<Storage> Storages { get; set; }
    }
}
