using AutoMapper;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Features.Posts.Queries.VMs;
using EmpServiciosPublicos.Domain;
using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Queries.GetAllPosts
{
    public class GetAllPostsHandler : IRequestHandler<GetAllPostsQueries, List<GetAllPostsMV>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPostsHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetAllPostsMV>> Handle(GetAllPostsQueries request, CancellationToken cancellationToken)
        {
            var getAllAsync = await _unitOfWork
             .Repository<Post>()
             .GetAllAsync();

            return _mapper.Map<List<GetAllPostsMV>>(getAllAsync);
        }
    }
}
