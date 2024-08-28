using Microsoft.AspNetCore.Http;

namespace E_Commerce.Application.Abstractions.Services;

public interface IFileService
{
    //tuple nesne dondurduk
    Task<List<(string fileName, string path)>> UploadAsync(string rootPath, IFormFileCollection files);

    Task<bool> CopyFileAsync(string path, IFormFile file);
}

