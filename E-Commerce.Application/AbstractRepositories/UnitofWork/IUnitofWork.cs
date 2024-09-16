using E_Commerce.Application.AbstractRepositories;
using E_Commerce.Application.AbstractRepositories;
using E_Commerce.Application.AbstractRepositories;
using E_Commerce.Application.Repositories;
using E_Commerce.Application.Repositories.Abstraction;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.AbstractRepositories.UnitofWork
{
    public interface IUnitofWork : IDisposable
    {

        IProductWriteRepository ProductWriteRepository { get; set; }
        IProductReadRepository ProductReadRepository { get; }

        IOrderWriteRepository OrderWriteRepository { get; set; }
        IOrderReadRepository OrderReadRepository { get; }

        ICustomerWriteRepository CustomerWriteRepository { get; set; }
        ICustomerReadRepository CustomerReadRepository { get; }

        IWriteFile WriteFile { get; set; }
        IReadFile ReadFile { get; }

        IWriteProductImageFile WriteProductImageFile { get; set; }
        IReadProductImageFile ReadProductImageFile { get; }

        IWriteInvoiceFile WriteInvoiceFile { get; set; }
        IReadInvoiceFile ReadInvoiceFile { get; }





        Task<int> SaveAsync();
    }
}

