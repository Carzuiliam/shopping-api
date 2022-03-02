using Microsoft.Data.Sqlite;
using shopping_api.Entities;
using shopping_api.Entities.Extended;
using shopping_api.Models;
using shopping_api.Utils;

namespace shopping_api.Handler.Default
{
    /// <summary>
    ///     Defines the corresponding handler for departments.
    /// </summary>
    public class DepartmentHandler : BaseHandler<Department>
    {
        /// <summary>
        ///     Lists all the departments in the database.
        /// </summary>
        /// 
        /// <returns>
        ///     A response with a list containing all the departments from the database.
        /// </returns>
        public Response<Department> List()
        {
            try
            {
                DepartmentEntity departmentEntity = new();

                Result.Data = departmentEntity.Select();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            return Result;
        }

        /// <summary>
        ///     Returns a specific department from the database.
        /// </summary>
        /// 
        /// <param name="_departmentId">The ID of a department.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding department from the database (if it exists).
        /// </returns>
        public Response<Department> Get(int _departmentId)
        {
            try
            {
                DepartmentEntity departmentEntity = new();
                departmentEntity.Filters.Id = _departmentId;

                Result.Data = departmentEntity.Select();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            return Result;
        }
    }
}
