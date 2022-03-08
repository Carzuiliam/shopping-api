using Shopping_API.Entities.Base;
using Shopping_API.Entities.Extended;

namespace Shopping_API.Entities.Filters
{
    /// <summary>
    ///     Contains a mapping between SQL attributes for the "Cart" database object
    /// and the <see cref="CartEntity"/> class, which is utilized to filter values
    /// from the same database object.
    /// </summary>
    public class CartFilters
    {
        /// <summary>
        ///     The parent <see cref="CartEntity"/>.
        /// </summary>
        public CartEntity Entity { set; get; }

        ///     Internal fields of the class.
        private int _id = 0;
        private decimal _subtotal = decimal.Zero;
        private decimal _discount = decimal.Zero;
        private decimal _shipping = decimal.Zero;
        private decimal _total = decimal.Zero;
        private DateTime _createdAt = DateTime.Now;
        private int _userId = 0;
        private int _couponId = 0;

        /// <summary>
        ///     Creates a new <see cref="CartFilters"/> object.
        /// </summary>
        /// 
        /// <param name="_entity">The parent entity.</param>
        public CartFilters(CartEntity _entity)
        {
            Entity = _entity;
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="CartEntity"/>
        /// attribute.
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                Entity.QueryFilters.Add(new EntityField("CRT_ID", _id));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="CartEntity"/>
        /// attribute.
        /// </summary>
        public decimal Subtotal
        {
            get => _subtotal;
            set
            {
                _subtotal = value;
                Entity.QueryFilters.Add(new EntityField("CRT_SUBTOTAL", _subtotal));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="CartEntity"/>
        /// attribute.
        /// </summary>
        public decimal Discount
        {
            get => _discount;
            set
            {
                _discount = value;
                Entity.QueryFilters.Add(new EntityField("CRT_DISCOUNT", _discount));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="CartEntity"/>
        /// attribute.
        /// </summary>
        public decimal Shipping
        {
            get => _shipping;
            set
            {
                _shipping = value;
                Entity.QueryFilters.Add(new EntityField("CRT_SHIPPING", _shipping));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="CartEntity"/>
        /// attribute.
        /// </summary>
        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                Entity.QueryFilters.Add(new EntityField("CRT_TOTAL", _total));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="CartEntity"/>
        /// attribute.
        /// </summary>
        public DateTime CreatedAt
        {
            get => _createdAt;
            set
            {
                _createdAt = value;
                Entity.QueryFilters.Add(new EntityField("CRT_CREATED_AT", _createdAt));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="CartEntity"/>
        /// attribute.
        /// </summary>
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                Entity.QueryFilters.Add(new EntityField("USR_ID", _userId));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="CartEntity"/>
        /// attribute.
        /// </summary>
        public int CouponId
        {
            get => _couponId;
            set
            {
                _couponId = value;
                Entity.QueryFilters.Add(new EntityField("CPN_ID", _couponId));
            }
        }
    }
}
