using EmpServiciosPublicos.Domain.Common;

namespace EmpServiciosPublicos.Domain
{
    public class Category : BaseDomain
    {
        public Category()
        {
            Posts = new HashSet<Post>();
            Storages = new HashSet<Storage>();
        }

        public string? Icono { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Storage>? Storages { get; set; }
    }
}
