using EmpServiciosPublicos.Domain;
using EmpServiciosPublicos.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EmpServiciosPublicas.Infrastructure.Persistence
{
    public class EmpServiciosPublicosDbContext : DbContext
    {
        public EmpServiciosPublicosDbContext(DbContextOptions<EmpServiciosPublicosDbContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Audit>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        entry.Entity.Availability = true;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "system";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Bidding> Biddings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PQRSD> PQRSDs { get; set; }
        public DbSet<TenderProposal> TenderProposals { get; set; }
    }
}
