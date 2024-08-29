using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Application.AbstractRepositories.UnitofWork;
using E_Commerce.Application.Repositories;
using E_Commerce.Application.RequestParameters;
using E_Commerce.Application.ViewModels;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;
using System.Net;


namespace E_Commerce.API.Controllers
{
    [Route("api/testcontroller")]
    [ApiController]
    public class MyTestController : Controller
    {
        private readonly IProductReadRepository _productRead;
        private readonly IProductWriteRepository _productWrite;        
        private readonly IUnitofWork _unitofWork;
        //IWebHostEnvironment  calistigi ortam hakkinda bilgi saglar. Path yada appname gibi seylere erismemizi saglar.
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public MyTestController(IProductReadRepository productRead,
            IProductWriteRepository productWrite,
            IOrderWriteRepository orderWrite,
            ICustomerWriteRepository customerWrite,
            IUnitofWork unitofWork,
            IWebHostEnvironment webHostEnvironment
,
            IFileService fileService)
        {
            this._productRead = productRead;
            this._productWrite = productWrite;     
            this._unitofWork = unitofWork;
            this._webHostEnvironment = webHostEnvironment;
            this._fileService = fileService;
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
            try
            {
                //await Task.Delay(1000); // Burda response biraz beklettik.Spinneri yakalaya bilmek icin.
                /*
                 *pageleme isleminde clientin kactane veri var bilmesi izin olusturulmus degisken.
                 *Items per page sabit kaliyor calismiyor. totali bilmmemiz lazim. 
                 *ProductService de list icinde nasil karsilanacagi gorulerbilir.
                 */
                //var totalCount = await _productRead.GetAll(false).CountAsync();

                //var products = await _productRead.GetAll(false).Select(p => new
                //{
                //    p.Id,
                //    p.Name,
                //    p.Stock,
                //    p.Price,
                //    p.CreationDate,
                //    p.UpdateDate
                //})
                //    .Skip(pagination.Page * pagination.Size)
                //    .Take(pagination.Size).ToListAsync();

                ///*once skip onra take olmali*/
                ///*Skip atlar. 1*10 da ilk onu alir. 3*10 da 30 tane alir. bu sebeple carpma kullanildi. skipde size kadarini getir dedik.*/
                ///*yani size 10 sa ve page 2 ise son ve ilk onluyu atlayip ortadaki 10luyu getirceke ama size kadar.*/

            
                if (_unitofWork == null)
                {
                    Log.Warning("UnitOfWork is not initialized");
                    throw new Exception("UnitOfWork is not initialized.");
                }

                if (_unitofWork.ProductReadRepository == null)
                {
                    Log.Warning("UnitOfWork is not initialized");
                    throw new Exception("ProductReadRepository could not be initialized.");
                }

                /*totalCount ve product degisken isimleri clientta karsi ayni ismde olmaz ise veri cekilemiyor.*/
                /*Clinetda listComponent icindeki " const allProducts: { totalCount: number, products: List_Products[] } " */
                var totalCount = await _unitofWork.ProductReadRepository.GetAll(false).CountAsync();
                var products = await _unitofWork.ProductReadRepository.GetAll(false).Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Stock,
                    p.Price,
                    p.CreationDate,
                    p.UpdateDate
                })
                   .Skip(pagination.Page * pagination.Size)
                   .Take(pagination.Size).ToListAsync();

                return Ok(new
                {
                    products,
                    totalCount
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, " List operation could not be initialized ");
                throw new InvalidOperationException("List operation could not be initialized " + ex.Message);
            }

        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            try
            {
                //Read oldugu icin tracking false
                return Ok(await _unitofWork.ProductReadRepository.GetByIdAsync(id, false));

            }
            catch (Exception ex)
            {
                Log.Error(ex, " Product could not get by id ");
                throw new InvalidOperationException("Product could not get by id : " + ex.Message);
            }
        }

        [HttpPost("newprd")]
        public async Task<IActionResult> InsertProduct(VM_Create_Product model)
        {

            try
            {
                //await _productWrite.AddAsync(new()
                //{
                //    Name = model.Name,
                //    Stock = model.Stock,
                //    Price = model.Price,
                //});
                //await _productWrite.SaveAsync();


                await _unitofWork.ProductWriteRepository.AddAsync(new()
                {
                    Name = model.Name,
                    Stock = model.Stock,
                    Price = model.Price,
                });
                await _unitofWork.SaveAsync();
                //int cast ettik.Cevidik.201 donecek OK() da olabilirdi.
                return StatusCode((int)HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while creating product");
                throw new InvalidOperationException("An error occured while creating product" + ex.Message);
            }

        }


        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct(VM_Update_Product model)
        {
            try
            {
                //Id yi ayri olarak almak yerine once id si esit olani cekip sonra update islemi yaptik.
                //update fonk kullanmadik cunku EF de verinin contexten cekilmemsi update icin yeterlidir cunku islem track ediliyor.
                //SaveChanges yapilarak kayit edilir. 

                //Product product = await _productRead.GetByIdAsync(model.Id);
                Product product = await _unitofWork.ProductReadRepository.GetByIdAsync(model.Id);
                product.Stock = model.Stock;
                product.Price = model.Price;
                product.Name = model.Name;
                //await _productWrite.SaveAsync();
                await _unitofWork.SaveAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while updating product");
                throw new InvalidOperationException("An error occured while updating product" + ex.Message);
            }
        }

        [HttpDelete("deleteproduct/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                //await _productWrite.RemoveAsync(id);
                //await _productWrite.SaveAsync();

                await _unitofWork.ProductWriteRepository.RemoveAsync(id);
                await _unitofWork.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while deleting product");
                throw new InvalidOperationException("An error occured while deleting product" + ex.Message);
            }
        }

        //Bir post fonk oldugu icin birbirinden ayirmamiz gerekiyor bu sebeple action ekledik. angularda belirlenen actionun ismi gelecek.s     
        [HttpPost("[action]")]
        public async Task<ActionResult> Upload()
        {
            //todo Directory path not found hatasi. cozulmeli. 
            try
            {
                var result = await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
                return Ok(result);
            }
            catch (DirectoryNotFoundException ex)
            {
                Log.Error(ex, "Directory path not found.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Directory path not found: " + ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while uploading file!");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while uploading file: " + ex.Message);
            }
        }
    }
}
