using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.API.Controllers;
using E_Commerce.Application.ViewModels;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;


namespace xUnitTest
{
    //xunitin IClassFixture<T> test siniflarinda genel olarak kullanilacak setup islemlerimizi yapmamizisaglar.
    public class ProductControllerTest : IClassFixture<ControllerTestFixture>
    {

        private readonly ControllerTestFixture _fixture;
        public ProductControllerTest(ControllerTestFixture fixture)
        {
            _fixture = fixture;


        }

        /// <summary>
        /// Controllesr asnyc oldugu icin test fonk da async olmali.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ReturnsPaginatedProducts()
        {
            // Arrange



            var pagination = new Pagination { Page = 1, Size = 10 };
            var products = new List<Product>
        {
            new Product { Id = new Guid("d91bc910-7a21-43c2-8e18-5fce8fa960ae"), Name = "Product1", Stock = 100, Price = 10, CreationDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Product { Id = new Guid("d91bc910-7a22-43c2-8e18-5fce8fa961ae"), Name = "Product2", Stock = 200, Price = 20, CreationDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Product { Id = new Guid("d91bc910-7a23-43c2-8e18-5fce8fa962ae"), Name = "Product3", Stock = 300, Price = 30, CreationDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Product { Id = new Guid("d91bc910-7a24-43c2-8e18-5fce8fa963ae"), Name = "Product4", Stock = 400, Price = 40, CreationDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Product { Id = new Guid("d91bc910-7a25-43c2-8e18-5fce8fa964ae"), Name = "Product5", Stock = 500, Price = 50, CreationDate = DateTime.Now, UpdateDate = DateTime.Now }
        };

            _fixture.MockProductRead.Setup(service => service.GetAll(false)).Returns(products.AsQueryable());


            // Act
            var result = await _fixture.ProductController.Get(pagination);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            //var returnedValue = okResult.Value as JObject; // JSON nesnesi olarak işlem yapıyoruz

            Assert.NotNull(okResult.Value);

            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(okResult.Value);

            //var returnedValue = okResult.Value as dynamic;
            var returnedValue = JObject.Parse(jsonString);
            Assert.NotNull(returnedValue);

            Assert.Equal(products.Count, (int)returnedValue["totalCount"]);
            Assert.Equal(products
                .Skip(pagination.Page * pagination.Size)
                .Take(pagination.Size)
                .Count(), ((IEnumerable<dynamic>)returnedValue["products"]).Count());//dynamic dinamik turdeki verileri belirtmede kullanilir. Json dinamik turde verileri icerir.
                                                                                     //IEnumerable<T> linq sorgu ve extention metrodlarinda count where select gibi metodlaru kolleksiyonlar uzerinde kullanmamizi saglar. 


        }

        [Fact]
        public async Task ProductCreateTest()
        {
            //Arrange
            var products = new List<VM_Create_Product>
                {
                    new VM_Create_Product { Name = "Product1", Stock = 100, Price = 10 },
                    new VM_Create_Product { Name = "Product2", Stock = 200, Price = 20 },
                    new VM_Create_Product { Name = "Product3", Stock = 300, Price = 30 },
                    new VM_Create_Product { Name = "Product4", Stock = 400, Price = 40 },
                    new VM_Create_Product { Name = "Product5", Stock = 500, Price = 50 }
                };


            //Act

            //It.IsAny<T> Moq da tip dogrulamasi yapar. Gelen verinin Product ile esligini dogrular.
            _fixture.MockProductWrite.Setup(service => service.AddAsync(It.IsAny<Product>())).ReturnsAsync(true);

            foreach (var product in products)
            {
                await _fixture.ProductController.InsertProduct(product);

            }

            //Assert
            _fixture.MockProductWrite.Verify(service => service.AddAsync(It.IsAny<Product>()), Times.Exactly(products.Count));


        }

        [Fact]
        public async Task ProductGetByIdTest()
        {
            //Arrange
            var products = new List<Product>
                {
                    new Product { Id = new Guid("d91bc910-7a21-43c2-8e18-5fce8fa960ae"), Name = "Product1", Stock = 100, Price = 10, CreationDate = DateTime.Now, UpdateDate = DateTime.Now },
                    new Product { Id = new Guid("d91bc910-7a22-43c2-8e18-5fce8fa961ae"), Name = "Product2", Stock = 200, Price = 20, CreationDate = DateTime.Now, UpdateDate = DateTime.Now },
                    new Product { Id = new Guid("d91bc910-7a23-43c2-8e18-5fce8fa962ae"), Name = "Product3", Stock = 300, Price = 30, CreationDate = DateTime.Now, UpdateDate = DateTime.Now },
                };

            //Act
            foreach (var item in products)
            {
                _fixture.MockProductRead.Setup(service => service.GetByIdAsync(item.Id.ToString(), false))
                               .ReturnsAsync(item);
            }


            //Assert
            foreach (var item in products)
            {
                var result = await _fixture.ProductController.GetByID(item.Id.ToString());

                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnedProduct = Assert.IsType<Product>(okResult.Value);
                Assert.Equal(item.Id, returnedProduct.Id);
                Assert.Equal(item.Name, returnedProduct.Name);
                Assert.Equal(item.Stock, returnedProduct.Stock);
                Assert.Equal(item.Price, returnedProduct.Price);
            }


        }





    }
}
