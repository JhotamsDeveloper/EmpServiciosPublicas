namespace EmpServiciosPublicas.Aplication.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IPQRSDRepository PQRSDRepository { get; }
        IPostRepository PostRepository { get; }
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Complete();
    }
}
