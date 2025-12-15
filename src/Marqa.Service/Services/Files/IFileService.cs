using Microsoft.AspNetCore.Http;

public interface IFileService
{
    Task<(string FileName, string FilePath)> UploadAsync(IFormFile file, string folder);

    bool IsImageExtension(string extension);
    bool IsVideoExtension(string extension);
}
