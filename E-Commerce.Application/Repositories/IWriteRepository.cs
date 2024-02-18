using E_Commerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntitiy
    {
        //ayni isimde, farkli tip alan func tanimlama islemine overload denir.
        Task<bool> AddAsync(T model);

        //Birden fazla ekleme islemi icin tanimladik. Bir koleksiyon eklemek istersek diye.
        Task<bool> AddRangeAsync(List<T> datas);

        bool Remove(T model);
        Task<bool> RemoveAsync(string id);

        bool RemoveRange(List<T> datas);

        bool Update(T model);
        //Task bool dondu. Yapilan islem tamamlandiysa true ya da false dondurmek yeter. 

        Task<int> SaveAsync();
    }
}
