﻿using Marqa.Service.Services;
using Marqa.Service.Services.Files.Models;
using Microsoft.AspNetCore.Http;

public interface IFileService : IScopedService
{
    Task<FileMetaData> UploadAsync(IFormFile file, string folder);
    bool IsImageExtension(string extension);
    bool IsVideoExtension(string extension);
}
