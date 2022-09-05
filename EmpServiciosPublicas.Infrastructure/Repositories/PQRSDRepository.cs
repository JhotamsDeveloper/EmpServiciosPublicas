using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Infrastructure.Persistence;
using EmpServiciosPublicos.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmpServiciosPublicas.Infrastructure.Repositories
{
    public class PQRSDRepository : RepositoryBase<PQRSD>, IPQRSDRepository
    {
        public PQRSDRepository(EmpServiciosPublicosDbContext context) : base(context)
        { }

        public async Task CreateAnonymousPQRSD(PQRSD pqrsd)
        {
            _ = await _context.PQRSDs.AddAsync(pqrsd);
            await _context.SaveChangesAsync();
        }

        public async Task CreatePQRSD(PQRSD pqrsd)
        {
            _ = await _context.PQRSDs.AddAsync(pqrsd);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PQRSD>> GetByType(string typePqrsd) =>
            await _context.PQRSDs!.Where(x => x.Type == typePqrsd).ToListAsync();


        public async Task<PQRSD> GetPQRSDByName(string name) => 
            await _context!.PQRSDs.FirstOrDefaultAsync(x => x.Title == name);
    }
}
