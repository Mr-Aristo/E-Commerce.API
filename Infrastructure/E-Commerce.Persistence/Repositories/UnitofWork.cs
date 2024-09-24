using E_Commerce.Application.AbstractRepositories;
using E_Commerce.Application.AbstractRepositories.UnitofWork;
using E_Commerce.Application.Repositories;
using E_Commerce.Infrastructure.Services.Storage;
using E_Commerce.Persistence.Concrete.Customers;
using E_Commerce.Persistence.Concrete.File;
using E_Commerce.Persistence.Concrete.Invoice;
using E_Commerce.Persistence.Concrete.Orders;
using E_Commerce.Persistence.Concrete.ProductImage;
using E_Commerce.Persistence.Concrete.Products;
using E_Commerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    //* Null exception aliyoruz henuz calismiyor *//
    public class UnitOfWork : IUnitofWork
    {
        private readonly ECommerceAPIContext _context;
        private ProductWriteRepository _productWriteRepository;
        private ProductReadRepository _productReadRepository;
        private OrderWriteRepository _orderWriteRepository;
        private OrderReadRepository _orderReadRepository;
        private CustomerWriteRepository _customerWriteRepository;
        private CustomerReadRepository _customerReadRepository;
        private WriteFile _writeFile;
        private ReadFile _readFile;
        private WriteProductImageFile _writeProductImage;
        private ReadProductImageFile _readProductImage;
        private WriteInvoiceFile _writeInvoice;
        private ReadInvoiceFile _readInvoice;
      //  private StorageService _storageService;
        public UnitOfWork(ECommerceAPIContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IProductWriteRepository ProductWriteRepository
        {
            get => _productWriteRepository ??= new ProductWriteRepository(_context);
            set => _productWriteRepository = (ProductWriteRepository)value;
        }

        public IProductReadRepository ProductReadRepository
        {
            get => _productReadRepository ??= new ProductReadRepository(_context);
        }

        public IOrderWriteRepository OrderWriteRepository
        {
            get => _orderWriteRepository ??= new OrderWriteRepository(_context);
            set => _orderWriteRepository = (OrderWriteRepository)value;
        }

        public IOrderReadRepository OrderReadRepository =>
            _orderReadRepository ??= new OrderReadRepository(_context);

        public ICustomerWriteRepository CustomerWriteRepository
        {
            get => _customerWriteRepository ??= new CustomerWriteRepository(_context);
            set => _customerWriteRepository = (CustomerWriteRepository)value;
        }

        public ICustomerReadRepository CustomerReadRepository =>
            _customerReadRepository ??= new CustomerReadRepository(_context);

        public IWriteFile WriteFile
        {
            get => _writeFile ??= new WriteFile(_context);
            set => _writeFile = (WriteFile)value;
        }

        public IReadFile ReadFile => _readFile ??= new ReadFile(_context);

        public IWriteProductImageFile WriteProductImageFile
        {
            get => _writeProductImage ??= new WriteProductImageFile(_context);
            set => _writeProductImage = (WriteProductImageFile)value;
        }

        public IReadProductImageFile ReadProductImageFile => _readProductImage ??= new ReadProductImageFile(_context);

        public IWriteInvoiceFile WriteInvoiceFile
        {
            get => _writeInvoice ??= new WriteInvoiceFile(_context);
            set => _writeInvoice = (WriteInvoiceFile)value;
        }

        public IReadInvoiceFile ReadInvoiceFile => _readInvoice ??= new ReadInvoiceFile(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}

