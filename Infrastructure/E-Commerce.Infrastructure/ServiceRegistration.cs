using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Application.Abstractions.Storage;
using E_Commerce.Infrastructure.Enums;
using E_Commerce.Infrastructure.Services;
using E_Commerce.Infrastructure.Services.Storage;
using E_Commerce.Infrastructure.Services.Storage.Azure;
using E_Commerce.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection service)
        {
            service.AddScoped<IStorageService, StorageService>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddSingleton<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection services, StorageType storageType)
        {

            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    services.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    break;
                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
