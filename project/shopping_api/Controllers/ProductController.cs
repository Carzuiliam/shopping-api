using Microsoft.AspNetCore.Mvc;
using shopping_api.Models;
using shopping_api.Utils;
using shopping_api.Handler.Default;

namespace shopping_api.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        [HttpGet("")]
        public Result<Product> List()
        {
            return new ProductHandler().List();
        }

        [HttpGet("{productId}")]
        public Result<Product> Get(int productId)
        {
            return new ProductHandler().Get(productId);
        }
    }
}
