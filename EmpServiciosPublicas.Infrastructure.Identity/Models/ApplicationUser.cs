using Microsoft.AspNetCore.Identity;

namespace EmpServiciosPublicas.Infrastructure.Identity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public string Surnames { get; set; }
    }
}
