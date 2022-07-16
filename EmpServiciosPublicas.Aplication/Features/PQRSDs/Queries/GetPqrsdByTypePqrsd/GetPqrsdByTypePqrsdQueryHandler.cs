using AutoMapper;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Queries.GetPqrsdByTypePqrsd
{
    public class GetPqrsdByTypePqrsdQueryHandler : IRequestHandler<GetPqrsdByTypePqrsdQuery, List<PqrsdMv>>
    {
        private readonly IPQRSDRepository _pqrsdRepository;
        private readonly IMapper _mapper;

        public GetPqrsdByTypePqrsdQueryHandler(IPQRSDRepository pqrsdRepository, IMapper mapper)
        {
            _pqrsdRepository = pqrsdRepository;
            _mapper = mapper;
        }

        public async Task<List<PqrsdMv>> Handle(GetPqrsdByTypePqrsdQuery request, CancellationToken cancellationToken)
        {
            var listPqrsd = await _pqrsdRepository.GetByType(request.TypePqrsd);
            return _mapper.Map<List<PqrsdMv>>(listPqrsd);
        }
    }
}
