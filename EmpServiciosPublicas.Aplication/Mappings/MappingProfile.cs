using AutoMapper;
using EmpServicioPublicos.Domain;
using EmpServiciosPublicas.Aplication.Features.PQRSD.Queries.GetPQRSDByCategory;

namespace EmpServiciosPublicas.Aplication.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<PQRSD, PqrsdMv>();
        }
    }
}
