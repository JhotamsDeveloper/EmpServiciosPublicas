using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.PQRSD.Queries.GetPQRSDByCategory
{
    public class GetPqrsdByCategoryQuery: IRequest<List<PqrsdMv>>
    {
        public string Category { get; set; }

        public GetPqrsdByCategoryQuery(string category)
        {
            Category = category ?? throw new ArgumentNullException(nameof(category));
        }
    }
}
