using EmpServiciosPublicas.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmpServiciosPublicas.Infrastructure.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AplicationUser>
    {
        public void Configure(EntityTypeBuilder<AplicationUser> builder)
        {
            var hasher = new PasswordHasher<AplicationUser>();

            builder.HasData(
                new AplicationUser
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
                new AplicationUser
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
