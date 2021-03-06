using AutoMapper;
using EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Queries.GetPqrsdByTypePqrsd;
using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PQRSD, PqrsdMv>().ReverseMap();
            CreateMap<PQRSD, CreateAnonymousCommand>().ReverseMap();
            CreateMap<Bidding, CreateBiddingCommand>().ReverseMap();
        }
    }
}
