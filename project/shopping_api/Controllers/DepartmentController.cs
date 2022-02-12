using Microsoft.AspNetCore.Mvc;
using shopping_api.Models;
using shopping_api.Utils;
using shopping_api.Handler.Default;

namespace shopping_api.Controllers
{
    [ApiController]
    [Route("department")]
    public class DepartmentController : ControllerBase
    {
        [HttpGet("")]
        public Result<Department> List()
        {
            return new DepartmentHandler().List();
        }

        [HttpGet("{departmentId}")]
        public Result<Department> Get(int departmentId)
        {
            return new DepartmentHandler().Get(departmentId);
        }
    }
}
