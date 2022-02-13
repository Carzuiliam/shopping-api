using Microsoft.AspNetCore.Mvc;
using shopping_api.Models;
using shopping_api.Utils;
using shopping_api.Handler.Default;

namespace shopping_api.Controllers
{
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        [HttpGet("")]
        public Result<Cart> List()
        {
            return new CartHandler().List();
        }

        [HttpGet("{cartId}")]
        public Result<Cart> Get(int cartId)
        {
            return new CartHandler().Get(cartId);
        }

        [HttpGet("user/{userId}")]
        public Result<Cart> GetFromUser(int userId)
        {
            return new CartHandler().GetFromUser(userId);
        }

        [HttpPut("{cartId}/product/{productId}")]
        public Result<Cart> AddProduct(int cartId, int productId)
        {
            return new CartHandler().AddProduct(cartId, productId);
        }

        [HttpPut("{cartId}/product/{productId}/quantity/{quantity}")]
        public Result<Cart> ChangeQuantity(int cartId, int productId, int quantity)
        {
            return new CartHandler().ChangeQuantity(cartId, productId, quantity);
        }

        [HttpDelete("{cartId}/product/{productId}")]
        public Result<Cart> RemoveProduct(int cartId, int productId)
        {
            return new CartHandler().RemoveProduct(cartId, productId);
        }

        [HttpDelete("{cartId}/product/all")]
        public Result<Cart> RemoveAll(int cartId)
        {
            return new CartHandler().RemoveAll(cartId);
        }

        [HttpPut("{cartId}/coupon/{coupon}")]
        public Result<Cart> ApplyCoupon(int cartId, string coupon)
        {
            return new CartHandler().ApplyCoupon(cartId, coupon);
        }

        [HttpDelete("{cartId}/coupon")]
        public Result<Cart> ClearCoupon(int cartId)
        {
            return new CartHandler().ClearCoupon(cartId);
        }
    }
}
