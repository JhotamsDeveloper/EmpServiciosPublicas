using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Contracts.Persistence
{
    public interface IPQRSDRepository : IAsyncRepository<PQRSD>
    {
        Task<string> CreateAnonymousPQRSD(PQRSD pqrsd);
        Task<string> CreatePQRSD(PQRSD pqrsd);
        Task<IEnumerable<PQRSD>> GetPQRSDByCategory(string category);
        Task<PQRSD> GetPQRSDByName(string Name);
    }
}
