using shopping_api.Entities.Default;
using Shopping_API.Entities.Filters;

namespace shopping_api.Entities.Extended
{
    public class ProductCartEntity : BaseEntity
    {
        public ProductCartRelations Relations { get; set; }

        public ProductCartFilters Filters { get; set; }

        public ProductCartValues Values { get; set; }

        public ProductCartEntity() : base("TB_PRODUCT_CART", "PRC_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        public class ProductCartRelations
        {
            public ProductCartEntity Entity { set; get; }

            public ProductCartRelations(ProductCartEntity _entity)
            {
                Entity = _entity;
            }

            public void Bind(BaseEntity _entity, EntityFilter.RelationType _relationType)
            {
                Entity.AddEntityFilter(_entity, _relationType);
            }
        }

        public class ProductCartFilters
        {
            public ProductCartEntity Entity { set; get; }

            private int _id = 0;
            private decimal _price = Decimal.Zero;
            private int _quantity = 0;
            private decimal _total = decimal.Zero;
            private DateTime _addedAt = DateTime.Now;
            private int _cartId = 0;
            private int _productId = 0;

            public ProductCartFilters(ProductCartEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryFilter("PRC_ID", _id);
                }
            }

            

            public decimal Price
            {
                get => _price;
                set
                {
                    _price = value;
                    Entity.AddQueryFilter("PRC_PRICE", _price);
                }
            }

            public int Quantity
            {
                get => _quantity;
                set
                {
                    _quantity = value;
                    Entity.AddQueryFilter("PRC_QUANTITY", _quantity);
                }
            }

            public decimal Total
            {
                get => _total;
                set
                {
                    _total = value;
                    Entity.AddQueryFilter("PRC_TOTAL", _total);
                }
            }

            public DateTime AddedAt
            {
                get => _addedAt;
                set
                {
                    _addedAt = value;
                    Entity.AddQueryFilter("PRC_ADDED_AT", _addedAt);
                }
            }

            public int CartId
            {
                get => _cartId;
                set
                {
                    _cartId = value;
                    Entity.AddQueryFilter("CRT_ID", _cartId);
                }
            }

            public int ProductId
            {
                get => _productId;
                set
                {
                    _productId = value;
                    Entity.AddQueryFilter("PRD_ID", _productId);
                }
            }
        }

        public class ProductCartValues
        {
            public ProductCartEntity Entity { set; get; }

            private int _id = 0;
            private decimal _price = Decimal.Zero;
            private int _quantity = 0;
            private decimal _total = decimal.Zero;
            private DateTime _addedAt = DateTime.Now;
            private int _cartId = 0;
            private int _productId = 0;

            public ProductCartValues(ProductCartEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryValue("PRC_ID", _id);
                }
            }
            
            public decimal Price
            {
                get => _price;
                set
                {
                    _price = value;
                    Entity.AddQueryValue("PRC_PRICE", _price);
                }
            }

            public int Quantity
            {
                get => _quantity;
                set
                {
                    _quantity = value;
                    Entity.AddQueryValue("PRC_QUANTITY", _quantity);
                }
            }

            public decimal Total
            {
                get => _total;
                set
                {
                    _total = value;
                    Entity.AddQueryValue("PRC_TOTAL", _total);
                }
            }

            public DateTime AddedAt
            {
                get => _addedAt;
                set
                {
                    _addedAt = value;
                    Entity.AddQueryValue("PRC_ADDED_AT", _addedAt);
                }
            }

            public int CartId
            {
                get => _cartId;
                set
                {
                    _cartId = value;
                    Entity.AddQueryValue("CRT_ID", _cartId);
                }
            }

            public int ProductId
            {
                get => _productId;
                set
                {
                    _productId = value;
                    Entity.AddQueryValue("PRD_ID", _productId);
                }
            }
        }
    }
}
