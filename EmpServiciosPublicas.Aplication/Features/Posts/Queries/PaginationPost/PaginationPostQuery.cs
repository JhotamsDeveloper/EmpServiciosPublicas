using EmpServiciosPublicas.Aplication.Features.Posts.Queries.VMs;
using EmpServiciosPublicas.Aplication.Features.Shared.Queries;
using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Queries.PaginationPost
{
    public class PaginationPostQuery : PaginationBaseQuery, IRequest<PaginationResponse<PostVm>>
    {
    }
}
