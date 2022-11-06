using AutoMapper;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Features.Posts.Queries.VMs;
using EmpServiciosPublicas.Aplication.Features.Shared.Queries;
using EmpServiciosPublicas.Aplication.Specifications.PostPaginationSettings;
using EmpServiciosPublicos.Domain;
using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Queries.PaginationPost
{
    public class PaginationPostHandler : IRequestHandler<PaginationPostQuery, PaginationResponse<PostVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationPostHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<PostVm>> Handle(PaginationPostQuery request, CancellationToken cancellationToken)
        {
            PostPaginationSettingsParams settingsParams = new()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
                
            };

            PostSpecification spec = new PostSpecification(settingsParams);
            IReadOnlyList<Post> posts = await _unitOfWork.Repository<Post>().GetAllWithSpec(spec);

            var sepcCount = new PostForCountingSpecification(settingsParams);
            var totalPosts = await _unitOfWork.Repository<Post>().CountAsync(sepcCount);
            var rounded = Math.Ceiling(Convert.ToDecimal(totalPosts) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<Post>, IReadOnlyList<PostVm>>(posts);
            var pagination = new PaginationResponse<PostVm>()
            {
                Count = totalPosts,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };

            return pagination;
        }
    }
}
