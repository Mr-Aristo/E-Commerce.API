using E_Commerce.Application.AbstractRepositories.UnitofWork;
using E_Commerce.Application.Repositories;
using E_Commerce.Persistence.Concrete.Customers;
using E_Commerce.Persistence.Concrete.Orders;
using E_Commerce.Persistence.Concrete.Products;
using E_Commerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
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

