using Microsoft.Data.Sqlite;
using Shopping_API.Entities.Base;
using Shopping_API.Entities.Connection;
using Shopping_API.Entities.Filters;
using Shopping_API.Entities.Relations;
using Shopping_API.Entities.Values;
using Shopping_API.Models;

namespace Shopping_API.Entities.Extended
{
    /// <summary>
    ///     Defines a custom entity, which represents the SQL object "Cart" from the SQL
    /// database.
    /// </summary>
    public class CartEntity : BaseEntity
    {
        /// <summary>
        ///     Contains the relations between the given <see cref="CartEntity"/>
        /// entity and other entities.
        /// </summary>
        public CartRelations Relations { get; set; }

        /// <summary>
        ///     Contains the filters of the given <see cref="CartEntity"/>.
        /// </summary>
        public CartFilters Filters { get; set; }

        /// <summary>
        ///     Contains the values for the given <see cref="CartEntity"/>.
        /// </summary>
        public CartValues Values { get; set; }

        /// <summary>
        ///     Creates a new <see cref="CartEntity"/> object (an extension of the
        /// <see cref="BaseEntity"/> object).
        /// </summary>
        public CartEntity() : base("TB_CART", "CRT_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        /// <summary>
        ///     Performs an insert of a "Cart" object into the database using the
        /// <see cref="EntityDB"/> object. Any value applied before the call of this
        /// method will affect the insert operation.
        /// </summary>
        /// 
        /// <param name="_entityDB">A target <see cref="EntityDB"/> object to perform the query.</param>
        /// 
        /// <returns>
        ///     <see cref="true"/> if inserted successfully, <see cref="false"/> otherwise.
        /// </returns>
        public bool Insert(EntityDB _entityDB)
        {
            return _entityDB.NonQuery(SQLInsert()) == 1;
        }

        /// <summary>
        ///     Selects a list of "Cart" objects from the database using an <see cref="EntityDB"/>
        /// object, returning them as carts. Any filter applied before the call of this method
        /// will affect the returned results.
        /// </summary>
        /// 
        /// <param name="_entityDB">A target <see cref="EntityDB"/> object to perform the query.</param>
        /// 
        /// <returns>
        ///     A list with a set of carts from a cart from the database.
        /// </returns>
        public List<Cart> Select(EntityDB _entityDB)
        {
            List<Cart> carts = new();

            using (var reader = _entityDB.Query(IsBinded ? SQLJoin() : SQLSelect()))
            {
                while (reader.Read())
                {
                    Cart cart = new()
                    {
                        Id = reader.GetInt32(0),
                        Subtotal = reader.GetDecimal(1),
                        Discount = reader.GetDecimal(2),
                        Shipping = reader.GetDecimal(3),
                        Total = reader.GetDecimal(4),
                        CreatedAt = reader.GetDateTime(5)
                    };

                    if (IsBinded)
                    {
                        cart.User = new()
                        {
                            Id = reader.GetInt32(8),
                            Username = reader.GetString(9),
                            Name = reader.GetString(10)
                        };

                        if (!reader.IsDBNull(11))
                        {
                            cart.Coupon = new()
                            {
                                Id = reader.GetInt32(11),
                                Code = reader.GetString(12),
                                Description = reader.GetString(13),
                                Discount = reader.GetDecimal(14)
                            };
                        };
                    }

                    carts.Add(cart);
                }
            }

            ClearParameters();

            return carts;
        }

        /// <summary>
        ///     Performs an update on a "Cart" object from the database using the
        /// <see cref="EntityDB"/> object. Any value applied before the call of this
        /// method will affect the update operation.
        /// </summary>
        /// 
        /// <param name="_entityDB">A target <see cref="EntityDB"/> object to perform the query.</param>
        /// 
        /// <returns>
        ///     <see cref="true"/> if updated successfully, <see cref="false"/> otherwise.
        /// </returns>
        public bool Update(EntityDB _entityDB)
        {
            int updatedRows = _entityDB.NonQuery(SQLUpdate());

            ClearParameters();

            return updatedRows == 1;
        }
    }    
}
