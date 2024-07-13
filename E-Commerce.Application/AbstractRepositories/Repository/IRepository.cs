using E_Commerce.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Repositories.Abstraction
{
    /// <summary>
    /// Bu kismi en genel kullanilacak seyleri tanimlamada kullaniyoruz. 
    /// 
    /// </summary>
    public interface IRepository<T> where T : BaseEntitiy //Marker pattern 
    {
        DbSet<T> Table { get; }


    }
}
