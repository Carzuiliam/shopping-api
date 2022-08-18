using Shopping_API.Entities.Base;
using Shopping_API.Entities.Extended;

namespace Shopping_API.Entities.Filters
{
    /// <summary>
    ///     Contains a mapping between SQL attributes for the "Coupon" database object
    /// and the <see cref="CouponEntity"/> class, which is utilized to filter values
    /// from the same database object.
    /// </summary>
    public class CouponFilters
    {
        /// <summary>
        ///     The parent <see cref="CouponEntity"/>.
        /// </summary>
        public CouponEntity Entity { set; get; }

        ///     Internal fields of the class.
        private int _id = 0;
        private string _code = "";
        private string _description = "";
        private decimal _discount = decimal.Zero;

        /// <summary>
        ///     Creates a new <see cref="CouponFilters"/> object.
        /// </summary>
        /// 
        /// <param name="_entity">The parent entity.</param>
        public CouponFilters(CouponEntity _entity)
        {
            Entity = _entity;
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="CouponEntity"/>
        /// attribute.
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                Entity.QueryFilters.Add(new EntityField("CPN_ID", _id));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="CouponEntity"/>
        /// attribute.
        /// </summary>
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                Entity.QueryFilters.Add(new EntityField("CPN_CODE", _code));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="CouponEntity"/>
        /// attribute.
        /// </summary>
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                Entity.QueryFilters.Add(new EntityField("CPN_DESCRIPTION", _description));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="CouponEntity"/>
        /// attribute.
        /// </summary>
        public decimal Discount
        {
            get => _discount;
            set
            {
                _discount = value;
                Entity.QueryFilters.Add(new EntityField("CPN_DISCOUNT", _discount));
            }
        }
    }
}
