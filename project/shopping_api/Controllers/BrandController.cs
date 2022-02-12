using Microsoft.AspNetCore.Mvc;
using shopping_api.Models;
using shopping_api.Utils;
using shopping_api.Handler.Extended;

namespace shopping_api.Controllers
{
    [ApiController]
    [Route("brand")]
    public class BrandController : ControllerBase
    {
        [HttpGet("")]
        public Result<Brand> List()
        {
            return new BrandHandler().List();
        }

        [HttpGet("{brandId}")]
        public Result<Brand> Get(int brandId)
        {
            return new BrandHandler().Get(brandId);
        }
    }
}
