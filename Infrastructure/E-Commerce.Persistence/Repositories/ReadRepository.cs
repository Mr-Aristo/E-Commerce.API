using E_Commerce.Application.Repositories.Abstraction;
using E_Commerce.Domain.Entities.Common;
using E_Commerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{/// <summary>
/// " Tracking " islemi update delete insert gibi islemleri takip edip 
/// fiziksel veri tabanina gerekli sql yapiyi gonderen mekanizmadir. Bir nevi sorgu olusturur.
/// Calisma maliyeti dusurmek icin read islmelerinde bu mekanizmayi 
/// kapatmak iyi olacaktir. 
/// </summary>
/// <typeparam name="T"></typeparam>
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntitiy
    {
        private readonly ECommerceAPIContext _context;

        public ReadRepository(ECommerceAPIContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();


        public IQueryable<T> GetAll(bool tracking = true) //=> Table; Iqueryable oldugundan dolayi tabloyu direk dondurebiliyoruz. 
        {
            //tracking isleminin manipulasyonu yapildi. 
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true) // => Table.Where(expression);
        {
            var query = Table.Where(expression);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true)//=> await Table.FirstOrDefaultAsync(expression)
        {
            var query = Table.AsQueryable();
            if(!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(expression);
        }
        

        //Generic calistigimiz icin elde bir id yok. Marker Pattern uygulayacagiz.
        //Entitylerimizi baseEntitiy ile isaretledigimiz icin  where T : class yerine BaseEntitiy tanimladik
        //Bu isleme marker pattern denir.  Isaretleyici illa class olmak zorunda degil interface de olabilir.
        //Normalde id ye erisemiyorduk reflection pattern yerine marker p. ile fazla kod maliyetine girmeden assagidaki sekilde tanimlamayi yaptik.

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        //=> Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id)); //=> marker pattern ORM de find fonk yoksa bu patern kullanilabilir.
        // => await Table.FindAsync(Guid.Parse(id));

        {//Bu kisimda Asqueryable da calistigimiz icin findasync yok. Marker pattern kullanilmali
            var query = Table.AsQueryable();
            if(!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));// Marker pattern. BaseEntity has Id
        }
    }


}
