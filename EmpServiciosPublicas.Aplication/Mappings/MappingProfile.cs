using EmpServiciosPublicas.Aplication.Features.PQRSDs.Queries.GetPqrsdByTypePqrsd;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.UpdateAnonymous;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous;
using EmpServiciosPublicas.Aplication.Features.Categories.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Update;
using EmpServiciosPublicos.Domain;
using AutoMapper;

namespace EmpServiciosPublicas.Aplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PQRSD, PqrsdMv>().ReverseMap();
            CreateMap<PQRSD, UpdateCommand>().ReverseMap();
            CreateMap<PQRSD, CreateCommand>().ReverseMap();
            CreateMap<PQRSD, CreateAnonymousCommand>().ReverseMap();
            CreateMap<PQRSD, UpdateAnonymousCommand>().ReverseMap();
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Bidding, CreateBiddingCommand>().ReverseMap();
        }
    }
}
