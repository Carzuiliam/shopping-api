using Shopping_API.Entities.Base;
using Shopping_API.Entities.Relations;
using Shopping_API.Entities.Filters;
using Shopping_API.Entities.Values;
using Shopping_API.Models;
using Shopping_API.Entities.Connection;

namespace Shopping_API.Entities.Extended
{
    /// <summary>
    ///     Defines a custom entity, which represents the SQL object "Coupon" from the SQL
    /// database.
    /// </summary>
    public class CouponEntity : BaseEntity
    {
        /// <summary>
        ///     Contains the relations between the given <see cref="CouponEntity"/>
        /// entity and other entities.
        /// </summary>
        public CouponRelations Relations { get; set; }

        /// <summary>
        ///     Contains the filters of the given <see cref="CouponEntity"/>.
        /// </summary>
        public CouponFilters Filters { get; set; }

        /// <summary>
        ///     Contains the values for the given <see cref="CouponEntity"/>.
        /// </summary>
        public CouponValues Values { get; set; }

        /// <summary>
        ///     Creates a new <see cref="CouponEntity"/> object (an extension of the
        /// <see cref="BaseEntity"/> object).
        /// </summary>
        public CouponEntity() : base("TB_COUPON", "CPN_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
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
        public List<Coupon> Select(EntityDB _entityDB)
        {
            List<Coupon> coupons = new();

            using (var reader = _entityDB.Query(IsBinded ? SQLJoin() : SQLSelect()))
            {
                while (reader.Read())
                {
                    Coupon cart = new()
                    {
                        Id = reader.GetInt32(0),
                        Code = reader.GetString(1),
                        Description = reader.GetString(2),
                        Discount = reader.GetDecimal(3)
                    };

                    coupons.Add(cart);
                }
            }

            ClearParameters();

            return coupons;
        }
    }
}
