using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.AbstractRepositories;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Context;
using E_Commerce.Persistence.Repositories;

namespace E_Commerce.Persistence.Concrete.ProductImage
{
    public class WriteProductImageFile : WriteRepository<ProductImageFile>, IWriteProductImageFile
    {
        public WriteProductImageFile(ECommerceAPIContext context) : base(context)
        {

        }
    }
}
