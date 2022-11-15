using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Models
{
    public class PostResponse
    {
        public Guid? Id { get; set; }
        public string Title { get; set; } = default!;
        public string Descrption { get; set; } = default!;
        public string Url { get; set; } = default!;
        public virtual CategoryMV Category { get; set; } = default!;
        public ICollection<StorageMV>? Storages { get; set; }
        public string State { get; set; } = default!;
    }
}
