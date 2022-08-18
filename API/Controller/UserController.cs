using Microsoft.AspNetCore.Mvc;
using Shopping_API.Api.Handler;
using Shopping_API.Models;
using Shopping_API.Utils;

namespace Shopping_API.Api.Controller
{
    /// <summary>
    ///     Defines a corresponding controller for users.
    /// </summary>
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        /// <summary>
        ///     Lists all the users in the database.
        /// </summary>
        /// 
        /// <returns>
        ///     A response with a list containing all the users from the database.
        /// </returns>
        [HttpGet("")]
        public Response<User> List()
        {
            return new UserHandler().List();
        }

        /// <summary>
        ///     Returns a specific user from the database.
        /// </summary>
        /// 
        /// <param name="_userId">The ID of a user.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding user from the database (if it exists).
        /// </returns>
        [HttpGet("{userId}")]
        public Response<User> Get(int userId)
        {
            return new UserHandler().Get(userId);
        }
    }
}
