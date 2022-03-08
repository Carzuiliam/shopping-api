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
    ///     Defines a custom entity, which represents the SQL object "ProductCart" from the SQL
    /// database.
    /// </summary>
    public class ProductCartEntity : BaseEntity
    {
        /// <summary>
        ///     Contains the relations between the given <see cref="ProductCartEntity"/>
        /// entity and other entities.
        /// </summary>
        public ProductCartRelations Relations { get; set; }

        /// <summary>
        ///     Contains the filters of the given <see cref="ProductCartEntity"/>.
        /// </summary>
        public ProductCartFilters Filters { get; set; }

        /// <summary>
        ///     Contains the values for the given <see cref="ProductCartEntity"/>.
        /// </summary>
        public ProductCartValues Values { get; set; }

        /// <summary>
        ///     Creates a new <see cref="ProductCartEntity"/> object (an extension
        /// of the <see cref="BaseEntity"/> object).
        /// </summary>
        public ProductCartEntity() : base("TB_PRODUCT_CART", "PRC_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        /// <summary>
        ///     Selects a list of "ProductCart" objects from the database, returning them as
        /// products in a cart. Any filter applied before the call of this method will affect
        /// the returned results.
        /// </summary>
        /// 
        /// <returns>
        ///     A list with a set of products in a cart from the database.
        /// </returns>
        public List<ProductCart> Select()
        {
            List<ProductCart> productsCart = new();
            EntityDB entityDB = new();

            entityDB.Start();

            using (var reader = entityDB.Query(IsBinded ? SQLJoin() : SQLSelect()))
            {
                while (reader.Read())
                {
                    ProductCart productCart = new()
                    {
                        Id = reader.GetInt32(0),
                        Price = reader.GetDecimal(1),
                        Quantity = reader.GetInt32(2),
                        Total = reader.GetDecimal(3),
                        AddedAt = reader.GetDateTime(4),
                        CartId = reader.GetInt32(5),
                        ProductId = reader.GetInt32(6)
                    };

                    if (IsBinded)
                    {
                        productCart.Product = new()
                        {
                            Id = reader.GetInt32(7),
                            Code = reader.GetString(8),
                            Name = reader.GetString(9),
                            Price = reader.GetDecimal(10),
                            Stock = reader.GetInt32(11)
                        };
                    }

                    productsCart.Add(productCart);
                }
            }
 
            ClearParameters();

            return productsCart;
        }
    }
}
