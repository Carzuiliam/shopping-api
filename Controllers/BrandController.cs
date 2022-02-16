using Microsoft.AspNetCore.Mvc;
using shopping_api.Models;
using shopping_api.Utils;
using shopping_api.Handler.Extended;

namespace shopping_api.Controllers
{
    /// <summary>
    ///     Defines a corresponding controller for brands.
    /// </summary>
    [ApiController]
    [Route("brand")]
    public class BrandController : ControllerBase
    {
        /// <summary>
        ///     Lists all the brands in the database.
        /// </summary>
        /// 
        /// <returns>
        ///     A response with a list containing all the brands from the database.
        /// </returns>
        [HttpGet("")]
        public Response<Brand> List()
        {
            return new BrandHandler().List();
        }

        /// <summary>
        ///     Returns a specific brand from the database.
        /// </summary>
        /// 
        /// <param name="brandId">The ID of a brand.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding brand from the database (if it exists).
        /// </returns>
        [HttpGet("{brandId}")]
        public Response<Brand> Get(int brandId)
        {
            return new BrandHandler().Get(brandId);
        }
    }
}
