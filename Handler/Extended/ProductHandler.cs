using shopping_api.Entities.Extended;
using shopping_api.Models;
using shopping_api.Utils;
using Shopping_API.Entities.Attributes;

namespace shopping_api.Handler.Default
{
    /// <summary>
    ///     Defines the corresponding handler for products.
    /// </summary>
    public class ProductHandler : BaseHandler<Product>
    {
        /// <summary>
        ///     Lists all the products in the database.
        /// </summary>
        /// 
        /// <returns>
        ///     A response with a list containing all the products from the database.
        /// </returns>
        public Response<Product> List()
        {
            try
            {
                ProductEntity productEntity = new();
                productEntity.Relations.Bind(new BrandEntity(), EntityRelation.RelationMode.FULL);
                productEntity.Relations.Bind(new DepartmentEntity(), EntityRelation.RelationMode.FULL);

                Result.Data = productEntity.Select();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            return Result;
        }

        /// <summary>
        ///     Returns a specific product from the database.
        /// </summary>
        /// 
        /// <param name="_productId">The ID of a product.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding product from the database (if it exists).
        /// </returns>
        public Response<Product> Get(int _productId)
        {
            try
            {
                ProductEntity productEntity = new();
                productEntity.Relations.Bind(new BrandEntity(), EntityRelation.RelationMode.FULL);
                productEntity.Relations.Bind(new DepartmentEntity(), EntityRelation.RelationMode.FULL);
                productEntity.Filters.Id = _productId;

                Result.Data = productEntity.Select();                
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            return Result;
        }
    }
}
