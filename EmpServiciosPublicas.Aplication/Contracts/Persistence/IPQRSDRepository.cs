using EmpServicioPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Contracts.Persistence
{
    public interface IPQRSDRepository : IAsyncRepository<PQRSD>
    {
        Task<IEnumerable<PQRSD>> GetPQRSDByCategory(string category);
        Task<PQRSD> GetPQRSDByName(string Name);
    }
}
