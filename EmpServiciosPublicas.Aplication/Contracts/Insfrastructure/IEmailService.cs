using EmpServiciosPublicas.Aplication.Models;

namespace EmpServiciosPublicas.Aplication.Contracts.Insfrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
