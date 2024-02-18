using E_Commerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{/// <summary>
/// burada id veredik cunku inherit edilen classla genel degerleri tekrar yazmadan atamis olduk
/// </summary>
    public class Order : BaseEntitiy
    {
        public Guid CustomerId { get; set; } // bu id 
        public string? Description { get; set; }
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }
        public Customer Customer { get; set; } // buna bagli ef kendi foreign key aticak. Koymazsak kendi otomatik db de koyar. 

    }
}
