using EmpServiciosPublicas.Infrastructure.Identity.Configurations;
using EmpServiciosPublicas.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmpServiciosPublicas.Infrastructure.Identity
{
    public class EmpServiciosPublicasIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public EmpServiciosPublicasIdentityDbContext(DbContextOptions<EmpServiciosPublicasIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRolConfiguration());
        }
    }
}
