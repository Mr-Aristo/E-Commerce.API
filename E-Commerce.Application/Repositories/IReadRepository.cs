using E_Commerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Repositories
{/// <summary>
 /// Parametre tarcking islemini manipule etmek icin olusutruldu. 
 ///  track edilmesini istediginde parametreyi yollamak yeterli. Islem maliyeti azaldi.
 ///  Parametre true seklinde dondugu icin fonc kullanirken illa gondermek zorunda degilsin
 ///  track edilmesin istediginde false gondermen yeterlidir.
 ///  track edilip edilmeme islmei concrete de yapildi.
 /// </summary>
/// <typeparam name="T"></typeparam>
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntitiy 
    {
        //IQueyable sorgularda calisilirken kullanilir. IEnumarable inmemory de calisir.
         IQueryable<T> GetAll(bool tracking = true);                                      

        //Sartli sorgu. Where sorgusuna es degerdir.
        IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true);

        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
    
    
}
