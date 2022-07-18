using AutoMapper;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Queries.GetPqrsdByTypePqrsd;
using EmpServiciosPublicas.Aplication.Mappings;
using EmpServiciosPublicas.Aplication.UnitTests.Moq;
using EmpServiciosPublicas.Infrastructure.Repositories;
using Moq;
using Shouldly;
using Xunit;

namespace EmpServiciosPublicas.Aplication.UnitTests.Features.PQRSDs.Queries
{
    public class GetPqrsdByTypePqrsdQueryHandlerXUnitTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;

        public GetPqrsdByTypePqrsdQueryHandlerXUnitTest()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            MoqPQRSDRepository.AddDataPQRSDRepository(_unitOfWork.Object.EmpServiciosPublicosDbContext);
        }

        [Fact]
        public async Task GetByTypeList()
        {
            var commandHandler = new GetPqrsdByTypePqrsdQueryHandler(_mapper, _unitOfWork.Object);
            var request = new GetPqrsdByTypePqrsdQuery("Felicitaciones");
            var result = await commandHandler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<List<PqrsdMv>>();
            result.Count.ShouldBe(1);
        }
    }
}
