using EmpServiciosPublicas.Aplication.Features.Posts.Models;
using EmpServiciosPublicas.Aplication.Features.Shared.Queries;
using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Queries.PaginationGetAllInactivePost
{
    public class PaginationGetAllInactivePostQuery : PaginationBaseQuery, IRequest<PaginationResponse<PostResponse>>
    {
    }
}
