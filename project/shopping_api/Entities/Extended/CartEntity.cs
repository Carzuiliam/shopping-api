using shopping_api.Entities.Default;
using Shopping_API.Entities.Attributes;

namespace shopping_api.Entities.Extended
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
        ///     Defines an object that contains the relations between the <see cref="CartEntity"/>
        /// and other entities.
        /// </summary>
        public class CartRelations
        {
            /// <summary>
            ///     The parent <see cref="CartEntity"/>.
            /// </summary>
            public CartEntity Entity { set; get; }

            /// <summary>
            ///     Creates a new <see cref="CartRelations"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public CartRelations(CartEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     Adds (binds) an entity to the given <see cref="CartEntity"/>.
            /// </summary>
            /// 
            /// <param name="_entity">The entity to bind with the current entity.</param>
            /// <param name="_relationType">How the relation will be performed (full or optional).</param>
            public void Bind(BaseEntity _entity, EntityRelation.RelationMode _relationType)
            {
                Entity.AddEntityFilter(_entity, _relationType);
            }
        }

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
                    Entity.AddQueryFilter("CRT_ID", _id);
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
                    Entity.AddQueryFilter("CRT_SUBTOTAL", _subtotal);
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
                    Entity.AddQueryFilter("CRT_DISCOUNT", _discount);
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
                    Entity.AddQueryFilter("CRT_SHIPPING", _shipping);
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
                    Entity.AddQueryFilter("CRT_TOTAL", _total);
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
                    Entity.AddQueryFilter("CRT_CREATED_AT", _createdAt);
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
                    Entity.AddQueryFilter("USR_ID", _userId);
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
                    Entity.AddQueryFilter("CPN_ID", _couponId);
                }
            }
        }

        /// <summary>
        ///     Contains a mapping between SQL attributes for the "Cart" database object
        /// and the <see cref="CartEntity"/> class, utilized to set values to the attributes
        /// in the same database object.
        /// </summary>
        public class CartValues
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
            ///     Creates a new <see cref="CartValues"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public CartValues(CartEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="CartEntity"/>
            /// attribute.
            /// </summary>
            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddFieldValue("CRT_ID", _id);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="CartEntity"/>
            /// attribute.
            /// </summary>
            public decimal Subtotal
            {
                get => _subtotal;
                set
                {
                    _subtotal = value;
                    Entity.AddFieldValue("CRT_SUBTOTAL", _subtotal);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="CartEntity"/>
            /// attribute.
            /// </summary>
            public decimal Discount
            {
                get => _discount;
                set
                {
                    _discount = value;
                    Entity.AddFieldValue("CRT_DISCOUNT", _discount);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="CartEntity"/>
            /// attribute.
            /// </summary>
            public decimal Shipping
            {
                get => _shipping;
                set
                {
                    _shipping = value;
                    Entity.AddFieldValue("CRT_SHIPPING", _shipping);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="CartEntity"/>
            /// attribute.
            /// </summary>
            public decimal Total
            {
                get => _total;
                set
                {
                    _total = value;
                    Entity.AddFieldValue("CRT_TOTAL", _total);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="CartEntity"/>
            /// attribute.
            /// </summary>
            public DateTime CreatedAt
            {
                get => _createdAt;
                set
                {
                    _createdAt = value;
                    Entity.AddFieldValue("CRT_CREATED_AT", _createdAt);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="CartEntity"/>
            /// attribute.
            /// </summary>
            public int UserId
            {
                get => _userId;
                set
                {
                    _userId = value;
                    Entity.AddFieldValue("USR_ID", _userId);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="CartEntity"/>
            /// attribute.
            /// </summary>
            public int CouponId
            {
                get => _couponId;
                set
                {
                    _couponId = value;
                    Entity.AddFieldValue("CPN_ID", _couponId);
                }
            }
        }
    }
}
