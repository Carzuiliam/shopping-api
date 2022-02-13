using shopping_api.Entities.Default;
using Shopping_API.Entities.Filters;

namespace shopping_api.Entities.Extended
{
    public class CartEntity : BaseEntity
    {
        public CartRelations Relations { get; set; }

        public CartFilters Filters { get; set; }

        public CartValues Values { get; set; }

        public CartEntity() : base("TB_CART", "CRT_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        public class CartRelations
        {
            public CartEntity Entity { set; get; }

            public CartRelations(CartEntity _entity)
            {
                Entity = _entity;
            }

            public void Bind(BaseEntity _entity, EntityFilter.RelationType _relationType)
            {
                Entity.AddEntityFilter(_entity, _relationType);
            }
        }

        public class CartFilters
        {
            public CartEntity Entity { set; get; }

            private int _id = 0;
            private decimal _subtotal = decimal.Zero;
            private decimal _discount = decimal.Zero;
            private decimal _shipping = decimal.Zero;
            private decimal _total = decimal.Zero;
            private DateTime _createdAt = DateTime.Now;
            private int _userId = 0;
            private int _couponId = 0;

            public CartFilters(CartEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryFilter("CRT_ID", _id);
                }
            }

            public decimal Subtotal
            {
                get => _subtotal;
                set
                {
                    _subtotal = value;
                    Entity.AddQueryFilter("CRT_SUBTOTAL", _subtotal);
                }
            }

            public decimal Discount
            {
                get => _discount;
                set
                {
                    _discount = value;
                    Entity.AddQueryFilter("CRT_DISCOUNT", _discount);
                }
            }

            public decimal Shipping
            {
                get => _shipping;
                set
                {
                    _shipping = value;
                    Entity.AddQueryFilter("CRT_SHIPPING", _shipping);
                }
            }

            public decimal Total
            {
                get => _total;
                set
                {
                    _total = value;
                    Entity.AddQueryFilter("CRT_TOTAL", _total);
                }
            }

            public DateTime CreatedAt
            {
                get => _createdAt;
                set
                {
                    _createdAt = value;
                    Entity.AddQueryFilter("CRT_CREATED_AT", _createdAt);
                }
            }

            public int UserId
            {
                get => _userId;
                set
                {
                    _userId = value;
                    Entity.AddQueryFilter("USR_ID", _userId);
                }
            }

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

        public class CartValues
        {
            public CartEntity Entity { set; get; }

            private int _id = 0;
            private decimal _subtotal = decimal.Zero;
            private decimal _discount = decimal.Zero;
            private decimal _shipping = decimal.Zero;
            private decimal _total = decimal.Zero;
            private DateTime _createdAt = DateTime.Now;
            private int _userId = 0;
            private int _couponId = 0;

            public CartValues(CartEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryValue("CRT_ID", _id);
                }
            }

            public decimal Subtotal
            {
                get => _subtotal;
                set
                {
                    _subtotal = value;
                    Entity.AddQueryValue("CRT_SUBTOTAL", _subtotal);
                }
            }

            public decimal Discount
            {
                get => _discount;
                set
                {
                    _discount = value;
                    Entity.AddQueryValue("CRT_DISCOUNT", _discount);
                }
            }

            public decimal Shipping
            {
                get => _shipping;
                set
                {
                    _shipping = value;
                    Entity.AddQueryValue("CRT_SHIPPING", _shipping);
                }
            }

            public decimal Total
            {
                get => _total;
                set
                {
                    _total = value;
                    Entity.AddQueryValue("CRT_TOTAL", _total);
                }
            }

            public DateTime CreatedAt
            {
                get => _createdAt;
                set
                {
                    _createdAt = value;
                    Entity.AddQueryValue("CRT_CREATED_AT", _createdAt);
                }
            }

            public int UserId
            {
                get => _userId;
                set
                {
                    _userId = value;
                    Entity.AddQueryValue("USR_ID", _userId);
                }
            }

            public int CouponId
            {
                get => _couponId;
                set
                {
                    _couponId = value;
                    Entity.AddQueryValue("CPN_ID", _couponId);
                }
            }
        }
    }
}
