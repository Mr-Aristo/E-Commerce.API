using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Repositories.Abstraction;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.AbstractRepositories
{
    public interface IWriteFile : IWriteRepository<Files>
    {
    }
}
