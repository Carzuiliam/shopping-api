using Shopping_API.Entities.Connection;
using Shopping_API.Entities.Extended;
using Shopping_API.Models;
using Shopping_API.Utils;

namespace Shopping_API.Api.Handler
{
    /// <summary>
    ///     Defines the corresponding handler for users.
    /// </summary>
    public class UserHandler : BaseHandler<User>
    {
        /// <summary>
        ///     Lists all the users in the database.
        /// </summary>
        /// 
        /// <returns>
        ///     A response with a list containing all the users from the database.
        /// </returns>
        public Response<User> List()
        {
            EntityDB entityDB = new();

            try
            {
                entityDB.Start();

                UserEntity userEntity = new();

                Result.Data = userEntity.Select(entityDB);

                entityDB.Finish();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            return Result;
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
        public Response<User> Get(int _userId)
        {
            EntityDB entityDB = new();

            try
            {
                entityDB.Start();

                UserEntity userEntity = new();
                userEntity.Filters.Id = _userId;

                Result.Data = userEntity.Select(entityDB);

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
