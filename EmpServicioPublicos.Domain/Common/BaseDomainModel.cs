namespace EmpServicioPublicos.Domain.Common
{
    public class BaseDomainModel
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
