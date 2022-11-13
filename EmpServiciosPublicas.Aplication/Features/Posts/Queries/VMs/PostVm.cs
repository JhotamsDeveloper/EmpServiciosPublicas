using EmpServiciosPublicos.Domain;
using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Queries.VMs
{
    public class PostVm
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; } 
        public string? Descrption { get; set; }
        public Guid? CategoryId { get; set; }
        public virtual CategoryMV? Category { get; set; }
        public ICollection<Storage>? Storages { get; set; }
    }
}
