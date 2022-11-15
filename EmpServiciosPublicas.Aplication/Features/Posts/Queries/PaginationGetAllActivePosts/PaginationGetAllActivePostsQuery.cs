using EmpServiciosPublicas.Aplication.Features.Posts.Models;
using EmpServiciosPublicas.Aplication.Features.Shared.Queries;
using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Queries.PaginationGetAllActivePosts
{
    public class PaginationGetAllActivePostsQuery : PaginationBaseQuery, IRequest<PaginationResponse<PostResponse>>
    {
    }
}
