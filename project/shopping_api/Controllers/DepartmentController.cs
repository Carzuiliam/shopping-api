using Microsoft.AspNetCore.Mvc;
using shopping_api.Models;
using shopping_api.Utils;
using shopping_api.Handler.Default;

namespace shopping_api.Controllers
{
    /// <summary>
    ///     Defines a corresponding controller for departments.
    /// </summary>
    [ApiController]
    [Route("department")]
    public class DepartmentController : ControllerBase
    {
        /// <summary>
        ///     Lists all the departments in the database.
        /// </summary>
        /// 
        /// <returns>
        ///     A response with a list containing all the departments from the database.
        /// </returns>
        [HttpGet("")]
        public Response<Department> List()
        {
            return new DepartmentHandler().List();
        }

        /// <summary>
        ///     Returns a specific department from the database.
        /// </summary>
        /// 
        /// <param name="departmentId">The ID of a department.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding department from the database (if it exists).
        /// </returns>
        [HttpGet("{departmentId}")]
        public Response<Department> Get(int departmentId)
        {
            return new DepartmentHandler().Get(departmentId);
        }
    }
}
