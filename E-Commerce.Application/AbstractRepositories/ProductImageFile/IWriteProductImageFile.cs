using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Repositories.Abstraction;

namespace E_Commerce.Application.AbstractRepositories;
    public interface IWriteProductImageFile : IWriteRepository<Domain.Entities.ProductImageFile>
    {
    }

