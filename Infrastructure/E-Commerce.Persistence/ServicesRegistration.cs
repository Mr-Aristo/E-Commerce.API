﻿using E_Commerce.Application.AbstractRepositories.UnitofWork;
using E_Commerce.Application.Repositories;
using E_Commerce.Persistence.Concrete.Customers;
using E_Commerce.Persistence.Concrete.Orders;
using E_Commerce.Persistence.Concrete.Products;
using E_Commerce.Persistence.Context;
using E_Commerce.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace E_Commerce.Persistence
{/// <summary>
/// DI larin yapildigi class
/// </summary>
    public static class ServicesRegistration
    {
        /// <summary>
        /// DI lari Yapan Fonksiyon
        /// Persistece.ServiceRegistration
        /// </summary>
        /// <param name="service"></param>
        public static void AddPersistanceServices(this IServiceCollection service)
        {
            //Con.string islemini tek bir configuration class olusturarak erismis olduk.
            //ServiceLifetime.Singleton ekledik cunku context dispose ediliyor.Fark IRepo lari ayni anda cagirdigimizde birden fazla context objesi olusuyor.
            //Bu sebeple scoped degil singleton kullandik.
            //Not Scopped kullanilirsa Cotrollerde task kullanildigi taktirde problem yine cozulur.Controller icindeki fonk. asenkron olmadigindan, fonk icinde kullanilan
            //asenkron fok beklenmiyor ve obje dispose ediliyor. Hata bu sebeple ortaya cikiyor. 
            //Scopped daha sagiliklidir
            service.AddDbContext<ECommerceAPIContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            //service.AddDbContext<ECommerceAPIContext>(options=> options.UseSqlServer(Configuration.ConnectionString)); //MSSQL config
            
            service.AddScoped<IUnitofWork, UnitOfWork>();
            service.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            service.AddScoped<IOrderReadRepository, OrderReadRepository>();
            service.AddScoped<IProductReadRepository, ProductReadRepository>();
            service.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            service.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            service.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        }
    }

}

/*
    -Not kodun icine congig yazilmaz. 
 
    -AddTransient
    Transient lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.
     
   - AddScoped
    Scoped lifetime services are created once per request.
     
   - AddSingleton
    Singleton lifetime services are created the first time they are requested (or when ConfigureServices is run if you specify an instance there) and then every subsequent request will use the same instance.
   - Example : 

  services.AddTransient<ITransientService, OperationService>();
  services.AddScoped<IScopedService, OperationService>();
  services.AddSingleton<ISingletonService, OperationService>();
 
 */
