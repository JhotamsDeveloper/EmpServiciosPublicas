using AutoMapper;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Queries.GetByCategory
{
    public class GetPqrsdByCategoryQueryHandler : IRequestHandler<GetPqrsdByCategoryQuery, List<PqrsdMv>>
    {
        private readonly IPQRSDRepository _pqrsdRepository;
        private readonly IMapper _mapper;

        public GetPqrsdByCategoryQueryHandler(IPQRSDRepository pqrsdRepository, IMapper mapper)
        {
            _pqrsdRepository = pqrsdRepository;
            _mapper = mapper;
        }

        public async Task<List<PqrsdMv>> Handle(GetPqrsdByCategoryQuery request, CancellationToken cancellationToken)
        {
            var listPqrsd = await _pqrsdRepository.GetPQRSDByCategory(request.Category);
            return _mapper.Map<List<PqrsdMv>>(listPqrsd);
        }
    }
}
