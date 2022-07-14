using Microsoft.AspNetCore.Identity;

namespace EmpServiciosPublicas.Infrastructure.Identity.Models
{
    public class AplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public string Surnames { get; set; }
    }
}
