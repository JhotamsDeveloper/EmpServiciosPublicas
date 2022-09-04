using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Handlers
{
    public static class ExtensionFiles
    {
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

        public static bool ValidateFileSize(this ICollection<IFormFile> files, long size)
        {
            foreach (var file in files)
            {
                if (file.Length > size)
                    return false;
            }
            return true;
        }
    }
}
