using E_Commerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{/// <summary>
/// Order sinifiyla coka cok iliski var
/// Bu tur iliskide genellikle EF kendi ara tabloyu olusturur.
/// n -n iliskide  ara bir tablo sarttir.
/// </summary>
    public class Product : BaseEntitiy
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }

        public ICollection<Order> Orders { get; set;}
    }
}
