using EmpServicioPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Contracts.Persistence
{
    public interface IPostRepository: IAsyncRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostByCategory(string category);
    }
}
