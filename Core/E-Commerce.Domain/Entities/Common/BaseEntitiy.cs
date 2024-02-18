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
        public DateTime UpdateDate { get; set; }
    }
}
