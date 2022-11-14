using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Models
{
    public class PostResponse
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Descrption { get; set; }
        public virtual CategoryMV? Category { get; set; }
        public ICollection<StorageMV>? Storages { get; set; }
    }
}
