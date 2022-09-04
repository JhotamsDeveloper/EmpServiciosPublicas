using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Infrastructure.Persistence;
using System.Collections;

namespace EmpServiciosPublicas.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly EmpServiciosPublicosDbContext _context;

        private IPostRepository _postRepository;
        private IPQRSDRepository _pQRSDRepository;

        public IPQRSDRepository PQRSDRepository => _pQRSDRepository ??= new PQRSDRepository(_context);
        public IPostRepository PostRepository => _postRepository ??= new PostRepository(_context);

        public UnitOfWork(EmpServiciosPublicosDbContext context)
        {
            _context = context;
        }

        public EmpServiciosPublicosDbContext EmpServiciosPublicosDbContext => _context;

        public async Task<int> Complete()
        {
            int result = 0;
            using var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {
                result = await _context.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();

            }
            catch (Exception)
            {
                dbContextTransaction.Rollback();
            }
            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                Type reporitoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(reporitoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type]!;
        }
    }
}
