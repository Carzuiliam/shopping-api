using Shopping_API.Entities.Connection;
using Shopping_API.Entities.Extended;
using Shopping_API.Models;
using Shopping_API.Utils;

namespace Shopping_API.Api.Handler
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
            EntityDB entityDB = new();

            try
            {
                entityDB.Start();

                DepartmentEntity departmentEntity = new();

                Result.Data = departmentEntity.Select(entityDB);
            
                entityDB.Finish();
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
            EntityDB entityDB = new();

            try
            {
                entityDB.Start();

                DepartmentEntity departmentEntity = new();
                departmentEntity.Filters.Id = _departmentId;

                Result.Data = departmentEntity.Select(entityDB);

                entityDB.Finish();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            return Result;
        }
    }
}
