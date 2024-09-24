using E_Commerce.Application.Validators.Products;
using E_Commerce.Infrastructure.Filters;
using E_Commerce.Infrastructure;
using E_Commerce.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using E_Commerce.Infrastructure.Services.Storage.Local;
using E_Commerce.Infrastructure.Services.Storage.Azure;
using E_Commerce.Infrastructure.Enums;

namespace E_Commerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /*  Add services to the container. */

            //CORS'un(Cross-Origin Resource Sharing) istegi onaylamaasi icin ekledigimiz middleware
            builder.Services.AddCors(options => options.AddDefaultPolicy(
                policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod()));

            // ServicesRegistration icinde bulunan Inject eden fonksiyon.
            builder.Services.AddPersistanceServices();
            builder.Services.AddInfrastructureServices();

            builder.Services.AddStorage<LocalStorage>();

            /*iki fakrli kullanimi*/
            //builder.Services.AddStorage<AzureStorage>();
            //builder.Services.AddStorage(StorageType.Azure);

            //Fluent validator tanimlandi. Birden fazla validator classi olabilir. Birtanesini tanimlamamiz sistemin diger
            //validator classlarinin otomatik olarak tanimlamasini saglayacaktir.
            builder.Services.AddControllers(options=> options.Filters.Add<ValidationFilterService>()) // => Assagida filteri bastirdik kendi filterimizi olusturduk
                .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
                //.net coreun kendi default vaild ilkesi oldugu icin girilen veriler valid degilse direkt controllere gelemeden
                //Cliente geri donduruyor. Biz bunu manuel yapmak istedigimiz icin asagidaki tanimlamayi yaptik.
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();//wwwroot dosyasini kullanabilmek icin eklemeliyiz. Middleware 
            //CORS
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}