using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace EmpServiciosPublicas.Aplication.Handlers
{
    public static class SerealizeExtension<T> where T : class
    {
        public static string Serealize(T model, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(model, options ?? new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            });
        }
    }
}
