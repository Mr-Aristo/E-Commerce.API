﻿using E_Commerce.Application.Repositories;
using E_Commerce.Application.RequestParameters;
using E_Commerce.Application.ViewModels;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

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
      

        //[HttpGet]
        //public async Task Get()
        //{
        //    await productWrite.AddRangeAsync(new()
        //    {                                                                           //.Now postgrede calismiyor UtcNow Olmali.
        //        new() { Id= Guid.NewGuid(),Name = "Product 1", Price=1000, CreationDate= DateTime.UtcNow, Stock=10},
        //        new() { Id= Guid.NewGuid(),Name = "Product 2", Price=2000, CreationDate= DateTime.UtcNow, Stock=10},
        //        new() { Id= Guid.NewGuid(),Name = "Product 3", Price=3000, CreationDate= DateTime.UtcNow, Stock=10},
        //        new() { Id= Guid.NewGuid(),Name = "Product 4", Price=4000, CreationDate= DateTime.UtcNow, Stock=10}

        //    });

        //    await productWrite.SaveAsync();
        //}

        //[HttpGet("order")]
        //public async Task GetOrder()
        //{
        //    var customerID = Guid.NewGuid();
        //    await customerWrite.AddAsync(new() { Id = customerID, Name = "Customer1" });
        //    await orderWrite.AddAsync(new() { Id = Guid.NewGuid(), Address = "ad1", Description = "des1", CustomerId = customerID });

        //    await orderWrite.SaveAsync();
        //}

        [HttpGet("productslist")]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination) //FromQuery fronttan query ile gelecegi icin tanimlandi.
        {
            /*
             *pageleme isleminde clientin kactane veri var bilmesi izin olusturulmus degisken.
             *Items per page sabit kaliyor calismiyor. totali bilmmemiz lazim. 
             *ProductService de list icinde nasil karsilanacagi gorulerbilir.
             */
            var totalCount = productRead.GetAll(false).Count();

            //Read oldugu icin tracking false
            //Binlerce verinin ayni anda sayfaya yuklenmemesi icin assagidaki gibi bir sorgu yaptik.
            var products = productRead.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreationDate,
                p.UpdateDate
            })
                .Skip(pagination.Page * pagination.Size)
                .Take(pagination.Size);
                
            /*once skip onra take olmali*/
            /*Skip atlar. 1*10 da ilk onu alir. 3*10 da 30 tane alir. bu sebeple carpma kullanildi. skipde size kadarini getir dedik.*/
            /*yani size 10 sa ve page 2 ise son ve ilk onluyu atlayip ortadaki 10luyu getirceke ama size kadar.*/



            return Ok(new
            {
                totalCount,
                products
                
            });

        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            //Read oldugu icin tracking false
            return Ok(await productRead.GetByIdAsync(id, false));

        }

        [HttpPost("newprd")]
        public async Task<IActionResult> InsertProduct(VM_Create_Product model)
        {


            await productWrite.AddAsync(new()
            {
                Name = model.Name,
                Stock = model.Stock,
                Price = model.Price,
            });
            await productWrite.SaveAsync();


            //int cast ettik.Cevidik.201 donecek OK() da olabilirdi.
            return StatusCode((int)HttpStatusCode.Created);


        }


        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct(VM_Update_Product model)
        {
            //Id yi ayri olarak almak yerine once id si esit olani cekip sonra update islemi yaptik.
            //update fonk kullanmadik cunku EF de verinin contexten cekilmemsi update icin yeterlidir cunku islem track ediliyor.
            //SaveChanges yapilarak kayit edilir. 
            Product product = await productRead.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;
            await productWrite.SaveAsync();

            return Ok();

        }

        [HttpDelete("deleteproduct/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await productWrite.RemoveAsync(id);
            await productWrite.SaveAsync();

            return Ok();
        }
    }
}
