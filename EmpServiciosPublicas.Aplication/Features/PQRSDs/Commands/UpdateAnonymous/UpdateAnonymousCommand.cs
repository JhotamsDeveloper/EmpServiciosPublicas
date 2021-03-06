using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.UpdateAnonymous
{
    public class UpdateAnonymousCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Descrption { get; set; } = string.Empty;
        public string PQRSDType { get; set; } = string.Empty;
    }
}
