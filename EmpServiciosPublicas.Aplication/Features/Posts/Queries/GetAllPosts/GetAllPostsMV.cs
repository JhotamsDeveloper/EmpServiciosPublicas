using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Queries.GetAllPosts
{
    public class GetAllPostsMV
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Descrption { get; set; } = default!;
    }
}
