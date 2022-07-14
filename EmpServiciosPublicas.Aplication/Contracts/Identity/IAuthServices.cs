using EmpServiciosPublicas.Aplication.Models.Identity;

namespace EmpServiciosPublicas.Aplication.Contracts.Identity
{
    public interface IAuthServices
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
