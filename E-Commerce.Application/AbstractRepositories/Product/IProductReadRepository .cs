﻿using E_Commerce.Application.Repositories.Abstraction;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Repositories
{
    public interface IProductReadRepository :IReadRepository<Product>
    {

    }
}
