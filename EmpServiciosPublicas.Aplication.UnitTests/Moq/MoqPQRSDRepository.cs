using AutoFixture;
using EmpServiciosPublicas.Infrastructure.Persistence;
using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.UnitTests.Moq
{
    public static class MoqPQRSDRepository
    {
        public static void AddDataPQRSDRepository(EmpServiciosPublicosDbContext pqrsdDbContextFace)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var pqrsds = fixture.CreateMany<PQRSD>().ToList();
            pqrsds.Add(fixture.Build<PQRSD>()
                .With(pq => pq.PQRSDType, "Felicitaciones")
                .Without(wt => wt.Storages)
                .Create());

            pqrsdDbContextFace.PQRSDs!.AddRange(pqrsds);
            pqrsdDbContextFace.SaveChanges();
        }
    }
}
