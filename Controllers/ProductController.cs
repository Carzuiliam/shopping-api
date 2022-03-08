using Microsoft.AspNetCore.Mvc;
using Shopping_API.Models;
using Shopping_API.Utils;
using Shopping_API.Handler.Default;

namespace Shopping_API.Controllers
{
    /// <summary>
    ///     Defines a corresponding controller for products.
    /// </summary>
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        /// <summary>
        ///     Lists all the products in the database.
        /// </summary>
        /// 
        /// <returns>
        ///     A response with a list containing all the products from the database.
        /// </returns>
        [HttpGet("")]
        public Response<Product> List()
        {
            return new ProductHandler().List();
        }

        /// <summary>
        ///     Returns a specific product from the database.
        /// </summary>
        /// 
        /// <param name="productId">The ID of a product.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding product from the database (if it exists).
        /// </returns>
        [HttpGet("{productId}")]
        public Response<Product> Get(int productId)
        {
            return new ProductHandler().Get(productId);
        }
    }
}
