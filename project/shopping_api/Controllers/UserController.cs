using Microsoft.AspNetCore.Mvc;
using shopping_api.Models;
using shopping_api.Utils;
using shopping_api.Handler.Default;

namespace shopping_api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        [HttpGet("")]
        public Result<User> List()
        {
            return new UserHandler().List();
        }

        [HttpGet("{userId}")]
        public Result<User> Get(int userId)
        {
            return new UserHandler().Get(userId);
        }
    }
}
