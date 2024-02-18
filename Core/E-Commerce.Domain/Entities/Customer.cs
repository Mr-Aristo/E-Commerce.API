using E_Commerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{/// <summary>
/// id vermedik inherit edilen classda "BaseEntitiy" kod tekrari yapmamak icin cekmis olduk
/// </summary>
    public class Customer: BaseEntitiy
    {
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }   

        


    }
}
