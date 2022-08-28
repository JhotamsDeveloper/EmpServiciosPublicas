namespace EmpServiciosPublicos.Domain.Common
{
    public abstract class Audit
    {
        public int? Id { get; set; }
        public bool Availability { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
