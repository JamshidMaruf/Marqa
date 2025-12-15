using Marqa.Service.Exceptions;
using Marqa.Service.Helpers;
using Microsoft.AspNetCore.Http;

namespace Marqa.Service.Services.Files;

public class FileService : IFileService
{
    public async Task<(string FileName, string FilePath)> UploadAsync(IFormFile file, string folder)
    {
        if (file is null || file.Length <= 0)
            throw new ArgumentIsNotValidException("File not uploaded");

        var extension = Path.GetExtension(file.FileName);
        var rootPath = Path.Combine(EnvironmentHelper.WebRootPath, folder);

        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);

        var fileName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(rootPath, fileName);

        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        return (fileName, fullPath);
    }

    public bool IsImageExtension(string extension) =>
        FileExtensionHelper.IsImageExtension(extension);

    public bool IsVideoExtension(string extension) =>
        FileExtensionHelper.IsVideoExtension(extension);
}
