using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace E_Commerce.Infrastructure.Services
{
    public class FileService : IFileService
    {
        readonly IWebHostEnvironment _webHostEnviroment;

        public FileService(IWebHostEnvironment webHostEnviroment)
        {
            _webHostEnviroment = webHostEnviroment;
        }
        public async Task<List<(string fileName, string path)>> UploadAsync(string rootPath, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnviroment.WebRootPath, rootPath);
            List<(string fileName, string path)> datas = new();
            List<bool> results = new();


            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath,file.FileName);

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                results.Add(result);
            }
            if (results.TrueForAll(r => r.Equals(true)))
                return datas;

            //todo if gecerli degilse sunucuda yuklenirken hata olusturuldu diye bir hata firlat.         
            return null;
        }
        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                //using obje isi bitince dispose eder. 
                await using FileStream fileStream = new(path,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    1024 * 1024,
                    useAsync: false);

                await fileStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

                return true;
            }
            catch (Exception ex)
            {
                //todo log!

                throw ex;
            }
        }

        //karmasik olan bu fonksiyonda yukklenen dosyadan ayni isimde iki tane varsa -1, -2 seklinde sayilandirma yapaagiz.
        //first true oldugunda ayni dosyadan yok demek, falsa girerse dosya adina ekleme yapacagiz. 
        private async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            await Task.Run(async () =>
             {
                 string extention = Path.GetExtension(fileName);
                 string newFileName = String.Empty;

                 if (first)
                 {
                     string oldName = Path.GetFileNameWithoutExtension(fileName);
                     newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extention}";
                 }
                 else
                 {
                     newFileName = fileName;
                     int indexNo1 = newFileName.IndexOf("-");
                     if (indexNo1 == -1)
                     {
                         newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extention}";
                     }
                     else
                     {
                         int indexNo2 = newFileName.IndexOf(".");
                         string fileNo = newFileName.Substring(indexNo1, indexNo2 - indexNo1 - 1);
                         //ahmet-123.png noktaya kadr 5 hepsi 9, 9-5 = 4, 4 noktaya geliyor birde -1 yaparak gelen sayiyi aldik.

                         int _fileNo = int.Parse(fileNo);
                         _fileNo++;
                         newFileName = newFileName.Remove(indexNo1, indexNo2 - indexNo1 - 1) // "-" den sonrasini silip _fileNo ile elde edilen sayiyi ekledik.
                         .Insert(indexNo1, _fileNo.ToString());
                     }
                 }



                 if (File.Exists($"{path}\\{newFileName}"))
                     return await FileRenameAsync(path, newFileName, false);
                 else
                     return newFileName;


             });

            return String.Empty;
        }


    }
}
