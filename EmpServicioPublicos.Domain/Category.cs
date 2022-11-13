using EmpServiciosPublicos.Domain.Common;

namespace EmpServiciosPublicos.Domain
{
    public class Category : BaseDomain
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public string? NameIcono { get; set; }
        public string? RouteIcono { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
