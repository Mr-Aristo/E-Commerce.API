using E_Commerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;//.net 6 yla gelen kutuphane build yapmadan json okumamizi sagliyor.Ayrica .json kutup. Yuklendi.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    /// <summary>
    /// Bu Configurationun olusturulma sebebi ReadMe de yazmakta.
    /// dotnet cli DesignTime error.
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIContext>
    {
        public ECommerceAPIContext CreateDbContext(string[] args)
        {


            #region Eski Configuration sinifini kullanan yapi.
           // DbContextOptionsBuilder<ECommerceAPIContext> dbContextOptions = new();

            //dbContextOptions.UseNpgsql(Configuration.ConnectionString);
            // dbContextOptions.UseSqlServer(Configuration.ConnectionString);

            //return new ECommerceAPIContext(dbContextOptions.Options); // read me de yazdigi gibi artik context const. ne alacagini biliyor 
            #endregion

            // IConfiguration'ı okuyarak appsettings.json içindeki connection string'e erişiyoruz.
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Bulunduğunuz klasörden başlar
                .AddJsonFile("appsettings.json")              // appsettings.json dosyasını ekler
                .Build();                                     // IConfigurationRoot objesini oluşturur

            // Connection string'i appsettings.json içinden alıyoruz
            var connectionString = configuration.GetConnectionString("PostgresConnection");

            DbContextOptionsBuilder<ECommerceAPIContext> dbContextOptions = new();

            // PostgreSQL bağlantısını kuruyoruz
            dbContextOptions.UseNpgsql(connectionString);

            // Eğer SQL Server kullanıyorsanız bu kısmı aktif edebilirsiniz
            // dbContextOptions.UseSqlServer(connectionString);

            return new ECommerceAPIContext(dbContextOptions.Options);

        }
    }
}                       
