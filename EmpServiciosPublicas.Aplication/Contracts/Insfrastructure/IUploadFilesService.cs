using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Contracts.Insfrastructure
{
    public interface IUploadFilesService
    {
        Task<(string nameFile, string path)> UploadedFileAsync(IFormFile file, string processType, string folder);
        Task<bool> DeleteUploadAsync(string nameFile, string processType, string folder);
    }
}
