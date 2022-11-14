using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Models
{
    public class GetAllPostsMV
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Descrption { get; set; } = default!;
    }
}
