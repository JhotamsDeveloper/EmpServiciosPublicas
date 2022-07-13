using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Queries.GetPqrsdByTypePqrsd
{
    public class GetPqrsdByTypePqrsdQuery: IRequest<List<PqrsdMv>>
    {
        public string TypePqrsd { get; set; }

        public GetPqrsdByTypePqrsdQuery(string typePqrsd)
        {
            TypePqrsd = typePqrsd;
        }
    }
}
