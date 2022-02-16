using Microsoft.Data.Sqlite;
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
            List<Product> products = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    ProductEntity productEntity = new();
                    productEntity.Relations.Bind(new BrandEntity(), EntityRelation.RelationMode.FULL);
                    productEntity.Relations.Bind(new DepartmentEntity(), EntityRelation.RelationMode.FULL);

                    SqliteCommand command = db.CreateCommand();
                    command.CommandText = productEntity.Join();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new()
                            {
                                Id = reader.GetInt32(0),
                                Code = reader.GetString(1),
                                Name = reader.GetString(2),
                                Price = reader.GetDecimal(3),
                                Stock = reader.GetInt32(4),
                                Brand = new()
                                {
                                    Id = reader.GetInt32(7),
                                    Code = reader.GetString(8),
                                    Name = reader.GetString(9)
                                },
                                Department = new()
                                {
                                    Id = reader.GetInt32(10),
                                    Name = reader.GetString(11)
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = products;

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
            List<Product> products = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    ProductEntity productEntity = new();
                    productEntity.Relations.Bind(new BrandEntity(), EntityRelation.RelationMode.FULL);
                    productEntity.Relations.Bind(new DepartmentEntity(), EntityRelation.RelationMode.FULL);
                    productEntity.Filters.Id = _productId;

                    SqliteCommand command = db.CreateCommand();
                    command.CommandText = productEntity.Join();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            products.Add(new()
                            {
                                Id = reader.GetInt32(0),
                                Code = reader.GetString(1),
                                Name = reader.GetString(2),
                                Price = reader.GetDecimal(3),
                                Stock = reader.GetInt32(4),
                                Brand = new()
                                {
                                    Id = reader.GetInt32(7),
                                    Code = reader.GetString(8),
                                    Name = reader.GetString(9)
                                },
                                Department = new()
                                {
                                    Id = reader.GetInt32(10),
                                    Name = reader.GetString(11)
                                }
                            });
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = products;

            return Result;
        }
    }
}
