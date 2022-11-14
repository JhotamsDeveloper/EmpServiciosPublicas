using EmpServiciosPublicas.Aplication.Features.Posts.Models;
using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Queries.GetAllPosts
{
    public class GetAllPostsQueries : IRequest<List<GetAllPostsMV>>
    {
        public GetAllPostsQueries()
        {

        }
    }
}
