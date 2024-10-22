using E_Commerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{ 
    /// <summary>
    /// Bu classin olustrulma amaci bu proje icinde ihtiyac duyuldugunda hizlica erisebilecegimiz bir config ilsemi yapmak.
    /// </summary>
    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {

                ConfigurationManager configurationManager = new();                             // ../ bir geri cikma anlamina gelir.
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/E-Commerce.API"));
                // yukledigimiz kutuphane sayesinde json a erisiyoruz. Bu kisim db config icin yapildi.
                configurationManager.AddJsonFile("appsettings.json"); 


                return configurationManager.GetConnectionString("PostgreSQL");
              //  return configurationManager.GetConnectionString("MsSQL");

            }
        }
    }
}
