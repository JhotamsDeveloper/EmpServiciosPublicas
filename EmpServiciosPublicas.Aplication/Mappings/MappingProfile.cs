using AutoMapper;
using EmpServiciosPublicos.Domain;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.UpdateAnonymous;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Queries.GetByCategory;

namespace EmpServiciosPublicas.Aplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PQRSD, PqrsdMv>().ReverseMap();
            CreateMap<PQRSD, CreateAnonymousCommand>().ReverseMap();
        }
    }
}
