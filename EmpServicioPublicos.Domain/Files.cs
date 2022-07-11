using EmpServiciosPublicos.Domain.Common;

namespace EmpServiciosPublicos.Domain
{
    public class Files: Audit
    {
        public string NameFile { get; set; }
        public string RouteFile { get; set; }
        public string Rol { get; set; }
    }
}
