using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace xUnitTest
{
    public class ProductControllerTest
    {

        /// <summary>
        /// Controllesr asnyc oldugu icin test fonk da async olmali.
        /// </summary>
        /// <returns></returns>
       // [Fact]
        //public async Task Get_ReturnsPaginatedProducts()
        //{
        //    // Arrange
        //    var mockProductRead = new Mock<IProductReadRepository>();
        //    var pagination = new Pagination { Page = 1, Size = 10 };

        //    var products = new List<Product>
        //{
        //    new Product { Id = Guid.NewGuid(), Name = "Product1", Stock = 100, Price = 10, CreationDate = DateTime.Now, UpdateDate = DateTime.Now }
        //};

        //    mockProductRead.Setup(service => service.GetAll(false)).Returns(products.AsQueryable());

        //    var controller = new MyTestController(mockProductRead.Object, null, null, null);

        //    // Act
        //    var result = await controller.Get(pagination);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var returnedValue = okResult.Value as dynamic;

        //    Assert.Equal(products.Count, (int)returnedValue.totalCount);
        //    Assert.Equal(products.Select(p => new
        //    {
        //        p.Id,
        //        p.Name,
        //        p.Stock,
        //        p.Price,
        //        p.CreationDate,
        //        p.UpdateDate
        //    }).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Count(), ((IEnumerable<dynamic>)returnedValue.products).Count());
        //}
        [Fact]
        public async Task Get_ReturnsPaginatedProducts()
        {
            // Arrange
            var mockProductRead = new Mock<IProductReadRepository>();
            var mockProductWrite = new Mock<IProductWriteRepository>();
            var mockOrderWrite = new Mock<IOrderWriteRepository>();
            var mockCustomerWrite = new Mock<ICustomerWriteRepository>();
   

            var pagination = new Pagination { Page = 1, Size = 10 };
            var productId = Guid.NewGuid();
            var products = new List<Product>
        {
            new Product { Id = productId, Name = "Product1", Stock = 100, Price = 10, CreationDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Product { Id = productId, Name = "Product2", Stock = 200, Price = 20, CreationDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Product { Id = productId, Name = "Product3", Stock = 300, Price = 30, CreationDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Product { Id = productId, Name = "Product4", Stock = 400, Price = 40, CreationDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Product { Id = productId, Name = "Product5", Stock = 500, Price = 50, CreationDate = DateTime.Now, UpdateDate = DateTime.Now }
        };

            mockProductRead.Setup(service => service.GetAll(false)).Returns(products.AsQueryable());

            /*Constructor 4 parametre aliyor. Mock ile o parametreleri gondermeliyiz.*/
            var controller = new MyTestController(mockProductRead.Object, mockProductWrite.Object, mockOrderWrite.Object, mockCustomerWrite.Object);

            // Act
            var result = await controller.Get(pagination);

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
                .Count(), ((IEnumerable<dynamic>)returnedValue["products"]).Count());
        }

    }
}
