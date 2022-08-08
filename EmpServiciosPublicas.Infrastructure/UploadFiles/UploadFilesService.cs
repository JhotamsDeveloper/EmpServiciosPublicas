using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Infrastructure.UploadFiles
{
    public class UploadFilesService : IUploadFilesService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public UploadFilesService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<(string nameFile, string path)> UploadedFileAsync(IFormFile file, string processType, string folder)
        {
            string uniqueFileName;

            string path = Path.Combine(_hostingEnvironment.WebRootPath, "Media" + "\\" + processType.ToString());
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(_hostingEnvironment.WebRootPath, "Media" + "\\" + processType.ToString() + "\\" + folder.ToString());
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            uniqueFileName = $"{Guid.NewGuid()}.{Path.GetExtension(file.FileName)[1..]}";
            string filePath = Path.Combine(path, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return (uniqueFileName, filePath);
        }

        public async Task<bool> DeleteUploadAsync(string nameFile, string processType, string folder)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "Media" + "\\" + processType.ToString() + "\\" + folder.ToString());
            FileInfo fileInfo = new(path);
            if (fileInfo != null)
            {
                System.IO.File.Delete(path);
                fileInfo.Delete();
                await Task.Yield();
                return true;
            }
            else
            {
                await Task.Yield();
                return false;
            }
        }
    }
}
