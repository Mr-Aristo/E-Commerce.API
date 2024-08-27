using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Context
{
    public class ECommerceAPIContext : DbContext
    {
        public ECommerceAPIContext(DbContextOptions<ECommerceAPIContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Bu kisimda  Interceptor yani her yaratilan veride createdDate ve updateDate'i yenilememek icin bir islem yapiyoruz
        /// Buradaki savechanges repository de kullanilanla ayni olmali.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker: Entitiler uzerinden yapilan degisikliklerin ya da yeni eklenen verinin yakalanmasini saglayan property dir. 
            //Update operasyionlarin tracke edilen verileri yakalayip elde etmemizi saglar. 

            //Burada base entity vermemizin sebebi onun genel olusu. createdatge bunun icinde ve diger butun entityler bu classi kullaniyor.
            //<T> Yakalanacak verinin classi
            var datas = ChangeTracker
                .Entries<BaseEntitiy>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreationDate = DateTime.UtcNow, // insert de bu ozellige veri gondermek zorunda degiliz. Otomatik kendisi takip edecek.
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                 

                    //delete isleminde 500 codu aldik sebebi deleteden sonra direk buraya girip son trackeri tetiklemesi. olmayan bir 
                    //seyi modify etmeye calisiyor, bu yuzden 500 aldik.
                    _ => DateTime.UtcNow
                };

            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        //Harnagi bir ekeleme ve ya update islemi yapildiginda bu fonksiyounu tetilersek veri yakalanacak ve tarihler otomatik olarak eklenecek. 
    }
}
