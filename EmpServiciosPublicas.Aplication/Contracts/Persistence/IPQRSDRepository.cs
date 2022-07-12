using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Contracts.Persistence
{
    public interface IPQRSDRepository : IAsyncRepository<PQRSD>
    {
        Task CreateAnonymousPQRSD(PQRSD pqrsd);
        Task CreatePQRSD(PQRSD pqrsd);
        Task<IEnumerable<PQRSD>> GetPQRSDByCategory(string category);
        Task<PQRSD> GetPQRSDByName(string name);
    }
}
