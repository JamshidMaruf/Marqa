using Microsoft.AspNetCore.Http;

namespace Marqa.Service.Services.Files;

public interface IFileService
{
    Task<(string FileName, string FilePath)> UploadAsync(IFormFile file, string folder);
}