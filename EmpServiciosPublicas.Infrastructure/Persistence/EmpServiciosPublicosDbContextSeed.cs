using EmpServiciosPublicos.Domain;
using System.Security.Claims;

namespace EmpServiciosPublicas.Infrastructure.Persistence
{
    public class EmpServiciosPublicosDbContextSeed
    {
        public string GetSessionUser()
        {
            var userName = _httpContextAccessor.HttpContext!.User?.Claims?
                                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return userName!;
        }

        public static IEnumerable<Category> GetPreconfiguredCategory()
        {
            return new List<Category>
            {
                new Category {Title = "Documentos", Descrption = "Esta sessión encontrarás todos los documentos púlicos de ínteres a la comunidad", Url = "documentos"},
                new Category {Title = "Noticias", Descrption = "Esta sessión encontrarás toda la información de los eventos, convocatirias e información de ínteres", Url = "noticias"},
            };

        }
    }
}
