using EmpServiciosPublicos.Domain.Common;

namespace EmpServiciosPublicos.Domain
{
    public class Category : BaseDomain
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public string? Icono { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
