using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Infrastructure.Persistence;
using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Infrastructure.Repositories
{
    public class PostRepository: RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(EmpServiciosPublicosDbContext context): base(context)
        { }
    }
}
