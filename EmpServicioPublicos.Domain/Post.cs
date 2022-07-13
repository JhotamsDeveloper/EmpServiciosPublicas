using EmpServiciosPublicos.Domain.Common;

namespace EmpServiciosPublicos.Domain
{
    public class Post: BaseDomain
    {
        public Post()
        {
            Storages = new HashSet<Storage>();
        }

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public ICollection<Storage> Storages { get; set; }
    }
}
