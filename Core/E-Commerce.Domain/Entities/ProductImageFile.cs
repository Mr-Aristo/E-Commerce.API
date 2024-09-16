using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{
    public class ProductImageFile : Files
    {
        public bool Showcase { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
