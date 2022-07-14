using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmpServiciosPublicas.Infrastructure.Identity.Configurations
{
    public class UserRolConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
               new IdentityUserRole<string>
               {
                   RoleId = "0bc5034c-bd6c-40ee-989d-07a6ec5f7ed7",
                   UserId = "ceeab6a9-228d-495e-bd39-b38eec5c2c29"
               },
               new IdentityUserRole<string>
               {
                   RoleId = "bf86422f-5674-4bca-9575-111da8cfdcca",
                   UserId = "92eaa36d-a589-4bc7-8db7-7428a1592cec"
               });
        }
    }
}
