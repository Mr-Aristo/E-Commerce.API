
using E_Commerce.Application.Abstractions.Storage;
using E_Commerce.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
            => File.Delete($"{pathOrContainerName}\\{fileName}");

        public List<string> GetFiles(string pathOrContainerName)
        {
            DirectoryInfo directoryInfo = new(pathOrContainerName);
            return directoryInfo.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
            => File.Exists($"{pathOrContainerName}\\{fileName}");

        async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {

                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log
                throw ex;
            }

        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainerName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();

            foreach (var file in files)
            {
                string fileNewName = await FileRenameAsync(pathOrContainerName, file.Name, HasFile);
                await CopyFileAsync($"{uploadPath}\\{file}", file);
                datas.Add((fileNewName, $"{pathOrContainerName}\\{fileNewName}"));

            }
            return datas;
        }


    }
}
