using EmpServiciosPublicas.Aplication.Constants;
using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Handlers
{
    public static class CustomExtensions
    {
        public static bool CheckNumber(this string name)
        {
            return name.All(Char.IsNumber);
        }

        public static bool IsEnumName(this string value)
        {
            bool result = false;

            Enum.GetValues(typeof(EnumPQRSD)).Cast<EnumPQRSD>().ToList().ForEach(item =>
            {
                if (item.ToString().ToUpper() == value.ToUpper())
                    result = true;
            });
            return result;
        }

        public static string GenericReference(this string value)
        {
            long tickes = DateTime.Now.Ticks;
            value = $"{value}_{tickes:20}";
            return value.ToLower();
        }

        public static bool ValidateCorrectFileFormat(this ICollection<IFormFile> files, string[] permittedExtensions)
        {
            foreach (var file in files)
            {
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!(!string.IsNullOrEmpty(ext) && permittedExtensions.Contains(ext)))
                    return false;
            }
            return true;
        }

        public static bool ValidateCorrectFileFormat(this IFormFile file, string[] permittedExtensions)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!(!string.IsNullOrEmpty(ext) && permittedExtensions.Contains(ext)))
                return false;

            return true;
        }

        public static bool ValidateFileSize(this ICollection<IFormFile> files, long size)
        {
            foreach (var file in files)
            {
                if (file.Length > size)
                    return false;
            }
            return true;
        }

        public static bool ValidateFileSize(this IFormFile file, long size)
        {
            if (file.Length > size)
                return false;

            return true;
        }

        public static string Create(this string value) =>
               string.Join("-", value!.Split('@', ',', '.', ';', '\'', ' ')).ToLower();
    }
}
