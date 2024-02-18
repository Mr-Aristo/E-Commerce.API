using E_Commerce.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/testcontroller")]
    [ApiController]
    public class MyTestController : Controller
    {
        private readonly IProductReadRepository productRead;
        private readonly IProductWriteRepository productWrite;
        private readonly IOrderWriteRepository orderWrite;
        private readonly ICustomerWriteRepository customerWrite;
        public MyTestController(IProductReadRepository productRead,
            IProductWriteRepository productWrite,
            IOrderWriteRepository orderWrite,
            ICustomerWriteRepository customerWrite)
        {
            this.productRead = productRead;
            this.productWrite = productWrite;
            this.orderWrite = orderWrite;
            this.customerWrite = customerWrite;
        }

        [HttpGet]
        public async Task Get()
        {
            await productWrite.AddRangeAsync(new()
            {                                                                           //.Now postgrede calismiyor UtcNow Olmali.
                new() { Id= Guid.NewGuid(),Name = "Product 1", Price=1000, CreationDate= DateTime.UtcNow, Stock=10},
                new() { Id= Guid.NewGuid(),Name = "Product 2", Price=2000, CreationDate= DateTime.UtcNow, Stock=10},
                new() { Id= Guid.NewGuid(),Name = "Product 3", Price=3000, CreationDate= DateTime.UtcNow, Stock=10},
                new() { Id= Guid.NewGuid(),Name = "Product 4", Price=4000, CreationDate= DateTime.UtcNow, Stock=10}

            });

            await productWrite.SaveAsync();
        }

        [HttpGet("order")]
        public async Task GetOrder()
        {
            var customerID = Guid.NewGuid();
            await customerWrite.AddAsync(new() { Id = customerID, Name = "Customer1" });
            await orderWrite.AddAsync(new() { Id = Guid.NewGuid(), Address = "ad1", Description = "des1", CustomerId = customerID });

            await orderWrite.SaveAsync();
        }


    }
}
