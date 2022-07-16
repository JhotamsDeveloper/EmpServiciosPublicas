using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Contracts.Persistence
{
    public interface IPQRSDRepository : IAsyncRepository<PQRSD>
    {
        Task CreateAnonymousPQRSD(PQRSD pqrsd);
        Task CreatePQRSD(PQRSD pqrsd);
        Task<IEnumerable<PQRSD>> GetByType(string typePqrsd);
        Task<PQRSD> GetPQRSDByName(string name);
    }
}
