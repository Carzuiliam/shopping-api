using Microsoft.Data.Sqlite;
using Shopping_API.Models;
using Shopping_API.Entities.Base;
using Shopping_API.Entities.Relations;
using Shopping_API.Entities.Filters;
using Shopping_API.Entities.Values;
using Shopping_API.Entities.Connection;

namespace Shopping_API.Entities.Extended
{
    /// <summary>
    ///     Defines a custom entity, which represents the SQL object "Product" from the SQL
    /// database.
    /// </summary>
    public class ProductEntity : BaseEntity
    {
        /// <summary>
        ///     Contains the relations between the given <see cref="ProductEntity"/>
        /// entity and other entities.
        /// </summary>
        public ProductRelations Relations { get; set; }

        /// <summary>
        ///     Contains the filters of the given <see cref="ProductEntity"/>.
        /// </summary>
        public ProductFilters Filters { get; set; }

        /// <summary>
        ///     Contains the values for the given <see cref="ProductEntity"/>.
        /// </summary>
        public ProductValues Values { get; set; }

        /// <summary>
        ///     Creates a new <see cref="ProductEntity"/> object (an extension of the
        /// <see cref="BaseEntity"/> object).
        /// </summary>
        public ProductEntity() : base("TB_PRODUCT", "PRD_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }
                
        /// <summary>
        ///     Selects a list of "Product" objects from the database, returning them as
        /// products. Any filter applied before the call of this method will affect the
        /// returned results.
        /// </summary>
        /// 
        /// <returns>
        ///     A list with a set of products from the database.
        /// </returns>
        public List<Product> Select()
        {
            List<Product> products = new();
            EntityDB entityDB = new();

            entityDB.Start();
            
            using (var reader = entityDB.Query(IsBinded ? SQLJoin() : SQLSelect()))
            {
                while (reader.Read())
                {
                    Product product = new()
                    {
                        Id = reader.GetInt32(0),
                        Code = reader.GetString(1),
                        Name = reader.GetString(2),
                        Price = reader.GetDecimal(3),
                        Stock = reader.GetInt32(4)
                    };

                    if (IsBinded)
                    {
                        product.Brand = new()
                        {
                            Id = reader.GetInt32(7),
                            Code = reader.GetString(8),
                            Name = reader.GetString(9)
                        };

                        product.Department = new()
                        {
                            Id = reader.GetInt32(10),
                            Name = reader.GetString(11)
                        };
                    }

                    products.Add(product);
                }
            }

            entityDB.Finish();

            ClearParameters();

            return products;
        }
    }
}
