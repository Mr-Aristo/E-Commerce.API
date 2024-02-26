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
    /// Bu Congigurationun olusturulma sebebi ReadMe de yazmakta.
    /// dotnet cli DesignTime error.
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIContext>
    {
        public ECommerceAPIContext CreateDbContext(string[] args)
        {
            

            DbContextOptionsBuilder<ECommerceAPIContext> dbContextOptions = new();
            dbContextOptions.UseNpgsql(Configuration.ConnectionString); 
           // dbContextOptions.UseSqlServer(Configuration.ConnectionString);

            return new ECommerceAPIContext(dbContextOptions.Options); // read me de yazdigi gibi artik context const. ne alacagini biliyor
        }
    }
}                       
