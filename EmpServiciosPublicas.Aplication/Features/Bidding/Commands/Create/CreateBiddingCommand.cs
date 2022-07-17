using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Create
{
    public class CreateBiddingCommand: IRequest<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Descrption { get; set; } = string.Empty;
        public DateTime StartOfTheCall { get; set; } = DateTime.Now;
        public DateTime EndOfTheCall { get; set; } 
    }
}
