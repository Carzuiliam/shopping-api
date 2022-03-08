using Shopping_API.Entities.Base;
using Shopping_API.Entities.Relations;
using Shopping_API.Entities.Filters;
using Shopping_API.Entities.Values;

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
    }
}
