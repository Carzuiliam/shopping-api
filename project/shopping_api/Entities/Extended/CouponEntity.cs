using shopping_api.Entities.Default;
using Shopping_API.Entities.Filters;

namespace shopping_api.Entities.Extended
{
    public class CouponEntity : BaseEntity
    {
        public CouponRelations Relations { get; set; }

        public CouponFilters Filters { get; set; }

        public CouponValues Values { get; set; }

        public CouponEntity() : base("TB_COUPON", "CPN_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        public class CouponRelations
        {
            public CouponEntity Entity { set; get; }

            public CouponRelations(CouponEntity _entity)
            {
                Entity = _entity;
            }

            public void Bind(BaseEntity _entity, EntityFilter.RelationType _relationType)
            {
                Entity.AddEntityFilter(_entity, _relationType);
            }
        }

        public class CouponFilters
        {
            public CouponEntity Entity { set; get; }

            private int _id = 0;
            private string _code = "";
            private string _description = "";
            private decimal _discount = decimal.Zero;

            public CouponFilters(CouponEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set 
                {
                    _id = value;
                    Entity.AddQueryFilter("CPN_ID", _id);
                }
            }

            public string Code
            {
                get => _code;
                set
                {
                    _code = value;
                    Entity.AddQueryFilter("CPN_CODE", _code);
                }
            }

            public string Description
            {
                get => _description;
                set
                {
                    _description = value;
                    Entity.AddQueryFilter("CPN_DESCRIPTION", _description);
                }
            }

            public decimal Discount
            {
                get => _discount;
                set
                {
                    _discount = value;
                    Entity.AddQueryFilter("CPN_DISCOUNT", _discount);
                }
            }
        }

        public class CouponValues
        {
            public CouponEntity Entity { set; get; }

            private int _id = 0;
            private string _code = "";
            private string _description = "";
            private decimal _discount = decimal.Zero;

            public CouponValues(CouponEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryValue("CPN_ID", _id);
                }
            }

            public string Code
            {
                get => _code;
                set
                {
                    _code = value;
                    Entity.AddQueryValue("CPN_CODE", _code);
                }
            }

            public string Description
            {
                get => _description;
                set
                {
                    _description = value;
                    Entity.AddQueryValue("CPN_DESCRIPTION", _description);
                }
            }

            public decimal Discount
            {
                get => _discount;
                set
                {
                    _discount = value;
                    Entity.AddQueryValue("CPN_DISCOUNT", _discount);
                }
            }
        }
    }
}
