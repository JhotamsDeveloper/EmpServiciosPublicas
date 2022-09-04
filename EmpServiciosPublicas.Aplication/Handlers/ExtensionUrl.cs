using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Handlers
{
    public static class ExtensionUrl
    {
        public static string Create(this string value) =>
            string.Join("-", value!.Split('@', ',', '.', ';', '\'', ' ')).ToLower();
    }
}
