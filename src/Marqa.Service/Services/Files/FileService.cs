using Marqa.Service.Exceptions;
using Marqa.Service.Helpers;
using Marqa.Service.Services.Files.Models;
using Marqa.Shared.Services;
using Microsoft.AspNetCore.Http;

namespace Marqa.Service.Services.Files;

public class FileService : IFileService
{
    private readonly IEnvironmentService _environmentService;

    public FileService(IEnvironmentService environmentService)
    {
        _environmentService = environmentService;
    }

    public async Task<FileMetaData> UploadAsync(IFormFile file, string folder)
    {
        if (file is null || file.Length <= 0)
            throw new ArgumentIsNotValidException("File not uploaded");

        var rootPath = Path.Combine(_environmentService.WebRootPath, folder);

        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(rootPath, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        return new FileMetaData
        {
            FileName = fileName,
            FilePath = fullPath
        };
    }

    public bool IsImageExtension(string extension) =>
        FileExtensionHelper.IsImageExtension(extension);

    public bool IsVideoExtension(string extension) =>
        FileExtensionHelper.IsVideoExtension(extension);
}
