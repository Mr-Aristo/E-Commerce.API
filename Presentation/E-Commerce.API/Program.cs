using E_Commerce.Persistence; 

namespace E_Commerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /*  Add services to the container. */

            //CORS'un istegi onaylamaasi icin ekledigimiz middleware
            builder.Services.AddCors(options => options.AddDefaultPolicy(
                policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod()));


            //builder.Services.AddCors(options => options.AddDefaultPolicy(
            //   policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            // ServicesRegistration icinde bulunan Inject eden fonksiyon.
            builder.Services.AddPersistanceServices(); 
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //CORS
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}