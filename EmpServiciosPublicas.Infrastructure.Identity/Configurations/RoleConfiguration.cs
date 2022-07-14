using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmpServiciosPublicas.Infrastructure.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
               new IdentityRole
               {
                   Id = "0bc5034c-bd6c-40ee-989d-07a6ec5f7ed7",
                   Name = "Administrator",
                   NormalizedName = "ADMINISTRATOR"

               },
               new IdentityRole
               {
                   Id = "bf86422f-5674-4bca-9575-111da8cfdcca",
                   Name = "Operator",
                   NormalizedName = "OPERATOR"

               });
        }
    }
}
