namespace EmpServiciosPublicos.Domain.Common
{
    public abstract class BaseDomain : Audit
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Descrption { get; set; }
    }
}
