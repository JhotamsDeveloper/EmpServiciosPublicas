using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicas.Aplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmpServiciosPublicas.Infrastructure.UploadFiles
{
    public class UploadFilesService : IUploadFilesService
    {
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly ILogger<UploadFilesService> _logger;
        private readonly IConfiguration _configuration;

        public UploadFilesService(IHostEnvironment hostingEnvironment, ILogger<UploadFilesService> logger, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<(string nameFile, string path)> UploadedFileAsync(IFormFile file, string processType, string folder)
        {
            string messageIlogger;
            string path;
            string uniqueFileName = $"{Guid.NewGuid()}.{Path.GetExtension(file.FileName)[1..]}";
            string storage = _configuration.GetSection("Storage:key").Value;
            if (string.IsNullOrEmpty(storage))
            {
                messageIlogger = "El appSetting no se ha configura la key storage:key";
                _logger.LogError(messageIlogger);
                throw new BadRequestException(messageIlogger);
            }
            try
            {
                path = Path.Combine(_hostingEnvironment.ContentRootPath, storage);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path = Path.Combine(_hostingEnvironment.ContentRootPath, storage, processType);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path = Path.Combine(_hostingEnvironment.ContentRootPath, storage, processType, folder);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path = Path.Combine(_hostingEnvironment.ContentRootPath, storage, processType, folder, uniqueFileName);

                using var stream = File.Create(path);
                await file.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
            string routeName = $"{storage}/{processType}/{folder}/{uniqueFileName}";
            return (uniqueFileName, routeName.ToLower());
        }

        public async Task<bool> DeleteUploadAsync(string nameFile, string processType, string folder)
        {
            //string path = Path.Combine(_hostingEnvironment.WebRootPath, "Media" + "\\" + processType.ToString() + "\\" + folder.ToString());
            //FileInfo fileInfo = new(path);
            //if (fileInfo != null)
            //{
            //    System.IO.File.Delete(path);
            //    fileInfo.Delete();
            //    await Task.Yield();
            //    return true;
            //}
            //else
            //{
            //    await Task.Yield();
            //    return false;
            //}

            return true;
        }
    }
}
