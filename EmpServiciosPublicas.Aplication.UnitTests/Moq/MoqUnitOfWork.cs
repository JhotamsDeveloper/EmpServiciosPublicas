using EmpServiciosPublicas.Infrastructure.Persistence;
using EmpServiciosPublicas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EmpServiciosPublicas.Aplication.UnitTests.Moq
{
    public static class MockUnitOfWork
    {
        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            var options = new DbContextOptionsBuilder<EmpServiciosPublicosDbContext>()
                .UseInMemoryDatabase(databaseName: $"EmpServiciosPublicas-{Guid.NewGuid()}")
                .Options;

            var unitOfWorkDbContextFace = new EmpServiciosPublicosDbContext(options);
            unitOfWorkDbContextFace.Database.EnsureDeleted();

            var mockUnitOfWork = new Mock<UnitOfWork>(unitOfWorkDbContextFace);
            return mockUnitOfWork;
        }
    }
}
