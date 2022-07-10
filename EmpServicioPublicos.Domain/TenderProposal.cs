using EmpServicioPublicos.Domain.Common;

namespace EmpServicioPublicos.Domain
{
    public class TenderProposal: Audit
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Surnames { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumer { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }

    }
}
