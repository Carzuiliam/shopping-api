using Microsoft.AspNetCore.Mvc;
using shopping_api.Models;
using shopping_api.Utils;
using shopping_api.Handler.Default;

namespace shopping_api.Controllers
{
    /// <summary>
    ///     Defines a corresponding controller for shopping carts.
    /// </summary>
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        /// <summary>
        ///     Lists all the carts in the database.
        /// </summary>
        /// 
        /// <returns>
        ///     A response with a list containing all the brands from the database.
        /// </returns>
        [HttpGet("")]
        public Response<Cart> List()
        {
            return new CartHandler().List();
        }

        /// <summary>
        ///     Returns a specific cart from the database.
        /// </summary>
        /// 
        /// <param name="cartId">The ID of a cart.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database (if it exists).
        /// </returns>
        [HttpGet("{cartId}")]
        public Response<Cart> Get(int cartId)
        {
            return new CartHandler().Get(cartId);
        }

        /// <summary>
        ///     Returns a specific cart from the database. This method differs from the
        /// <see cref="Get(int)"/> method because this one finds a cart based on a user ID.
        /// This method also creates a new cart for the given user if no cart was originally
        /// found.
        /// </summary>
        /// 
        /// <param name="userId">The ID of a user.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database (new or existing).
        /// </returns>
        [HttpGet("user/{userId}")]
        public Response<Cart> GetFromUser(int userId)
        {
            return new CartHandler().GetFromUser(userId);
        }

        /// <summary>
        ///     Adds a product to a cart. The product must exists in the database, and it
        /// must not be added previously to the same cart.
        /// </summary>
        /// 
        /// <param name="cartId">The ID of the cart.</param>
        /// <param name="productId">The ID of the product.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database (new or existing).
        /// </returns>
        [HttpPut("{cartId}/product/{productId}")]
        public Response<Cart> AddProduct(int cartId, int productId)
        {
            return new CartHandler().AddProduct(cartId, productId);
        }

        /// <summary>
        ///     Changes the quantity of a product in a cart. The quantity must not be zero, 
        /// negative, or greater than the original stock of the product.
        /// </summary>
        /// 
        /// <param name="cartId">The ID of the cart.</param>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="quantity">The new quantity for the product.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database.
        /// </returns>
        [HttpPut("{cartId}/product/{productId}/quantity/{quantity}")]
        public Response<Cart> ChangeQuantity(int cartId, int productId, int quantity)
        {
            return new CartHandler().ChangeQuantity(cartId, productId, quantity);
        }

        /// <summary>
        ///     Removes a product to a cart. The product must exists in the database, and it
        /// must be added to the cart before.
        /// </summary>
        /// 
        /// <param name="cartId">The ID of the cart.</param>
        /// <param name="productId">The ID of the product.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database.
        /// </returns>
        [HttpDelete("{cartId}/product/{productId}")]
        public Response<Cart> RemoveProduct(int cartId, int productId)
        {
            return new CartHandler().RemoveProduct(cartId, productId);
        }

        /// <summary>
        ///     Removes all the product of a cart. The cart will not be deleted from the
        /// database, though.
        /// </summary>
        /// 
        /// <param name="cartId">The ID of the cart.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database.
        /// </returns>
        [HttpDelete("{cartId}/product/all")]
        public Response<Cart> RemoveAll(int cartId)
        {
            return new CartHandler().RemoveAll(cartId);
        }

        /// <summary>
        ///     Applies a coupon to a cart, adding a discount to it. The coupon must exists,
        /// and any coupon added previously will be discarded.
        /// </summary>
        /// 
        /// <param name="cartId">The ID of the cart.</param>
        /// <param name="coupon">The code of the coupon.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database.
        /// </returns>
        [HttpPut("{cartId}/coupon/{coupon}")]
        public Response<Cart> ApplyCoupon(int cartId, string coupon)
        {
            return new CartHandler().ApplyCoupon(cartId, coupon);
        }

        /// <summary>
        ///     Clears the coupon from a cart.
        /// </summary>
        /// 
        /// <param name="cartId">The ID of the cart.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database.
        /// </returns>
        [HttpDelete("{cartId}/coupon")]
        public Response<Cart> ClearCoupon(int cartId)
        {
            return new CartHandler().ClearCoupon(cartId);
        }
    }
}
