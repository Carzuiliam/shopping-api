using Microsoft.Data.Sqlite;
using shopping_api.Entities.Extended;
using shopping_api.Handler.Default;
using shopping_api.Models;
using shopping_api.Utils;

namespace shopping_api.Handler.Extended
{
    /// <summary>
    ///     Defines the corresponding handler for brands.
    /// </summary>
    public class BrandHandler : BaseHandler<Brand>
    {
        /// <summary>
        ///     Lists all the brands in the database.
        /// </summary>
        /// 
        /// <returns>
        ///     A response with a list containing all the brands from the database.
        /// </returns>
        public Response<Brand> List()
        {
            try
            {
                BrandEntity brandEntity = new();

                Result.Data = brandEntity.Select();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            return Result;
        }

        /// <summary>
        ///     Returns a specific brand from the database.
        /// </summary>
        /// 
        /// <param name="_brandId">The ID of a brand.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding brand from the database (if it exists).
        /// </returns>
        public Response<Brand> Get(int _brandId)
        {
            try
            {
                BrandEntity brandEntity = new();
                brandEntity.Filters.Id = _brandId;

                Result.Data = brandEntity.Select();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            return Result;
        }
    }
}
