using EmpServiciosPublicas.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmpServiciosPublicas.Infrastructure.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                new ApplicationUser
                {
                    Id = "ceeab6a9-228d-495e-bd39-b38eec5c2c29",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Name = "Jhonatan Alejandro",
                    Surnames = "Muñoz Serna",
                    UserName = "JhotaMS",
                    NormalizedUserName = "JhotaMS",
                    PasswordHash = hasher.HashPassword(null, "adminLocalhost$"),
                    EmailConfirmed = true

                },
                new ApplicationUser
                {
                    Id = "92eaa36d-a589-4bc7-8db7-7428a1592cec",
                    Email = "alexa@localhost.com",
                    NormalizedEmail = "alexa@localhost.com",
                    Name = "Alexandra",
                    Surnames = "David Aguinaga",
                    UserName = "Alexa",
                    NormalizedUserName = "Alexa",
                    PasswordHash = hasher.HashPassword(null, "AlexaLocalhost$")
                }

            );
        }
    }
}
