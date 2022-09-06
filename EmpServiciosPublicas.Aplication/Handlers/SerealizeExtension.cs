using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmpServiciosPublicas.Aplication.Handlers
{
    public static class SerealizeExtension<T> where T : class
    {
        public static string Serealize(T model, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(model, options ?? new JsonSerializerOptions()
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            });
        }
    }
}
