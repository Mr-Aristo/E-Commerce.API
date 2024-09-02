using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/v{version:apiVersion}product")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController : Controller
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
