using EmpServiciosPublicos.Domain;
using Microsoft.Extensions.Logging;

namespace EmpServiciosPublicas.Infrastructure.Persistence
{
    public class EmpServiciosPublicosDbContextSeed
    {
        public static async Task SeedAsync(EmpServiciosPublicosDbContext context, ILogger<EmpServiciosPublicosDbContextSeed> logger)
        {
            if (!context.Categories!.Any())
            {
                context.Categories!.AddRange(GetPreconfiguredCategory());
                await context.SaveChangesAsync();
                logger.LogInformation("Estamos insertando nuevos records al db {context}", typeof(EmpServiciosPublicosDbContext).Name);
            }
        }

        private static IEnumerable<Category> GetPreconfiguredCategory()
        {
            return new List<Category>
            {
                new Category {Title = "Documentos", Descrption = "Esta sessión encontrarás todos los documentos púlicos de ínteres a la comunidad", Url = "documentos"},
                new Category {Title = "Noticias", Descrption = "Esta sessión encontrarás toda la información de los eventos, convocatirias e información de ínteres", Url = "noticias"},
            };

        }
    }
}
