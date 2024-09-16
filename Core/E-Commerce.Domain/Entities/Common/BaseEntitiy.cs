using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.Common
{
    public class BaseEntitiy
    {/// <summary>
     /// Common dosyasi altinda bu clasi olusturmamizin sebebi
     /// Diger entitilerde ortak olacak ozellikleri tek bir 
     /// yerde tanimlayarak kod tekrarindan kurtulmak
     /// </summary>
        public Guid Id { get; set; } //Guid is uniqe indentify
        public DateTime CreationDate { get; set; }

        //TPH icin kullanildi. Bu columun olmasini istemeigimiz classlarda cagirmayacaz. s
        virtual public DateTime UpdateDate { get; set; } 
    }
}
