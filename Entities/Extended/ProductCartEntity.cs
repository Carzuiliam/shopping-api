using shopping_api.Entities.Default;
using Shopping_API.Entities.Attributes;

namespace shopping_api.Entities.Extended
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
        ///     Defines an object that contains the relations between the <see cref="ProductCartEntity"/>
        /// and other entities.
        /// </summary>
        public class ProductCartRelations
        {
            /// <summary>
            ///     The parent <see cref="ProductCartEntity"/>.
            /// </summary>
            public ProductCartEntity Entity { set; get; }

            /// <summary>
            ///     Creates a new <see cref="ProductCartRelations"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public ProductCartRelations(ProductCartEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     Adds (binds) a entity to the given <see cref="ProductCartEntity"/>.
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
        ///     Contains a mapping between SQL attributes for the "ProductCart" database object
        /// and the <see cref="ProductCartEntity"/> class, which is utilized to filter values
        /// from the same database object.
        /// </summary>
        public class ProductCartFilters
        {
            /// <summary>
            ///     The parent <see cref="ProductCartEntity"/>.
            /// </summary>
            public ProductCartEntity Entity { set; get; }

            ///     Internal fields of the class.
            private int _id = 0;
            private decimal _price = decimal.Zero;
            private int _quantity = 0;
            private decimal _total = decimal.Zero;
            private DateTime _addedAt = DateTime.Now;
            private int _cartId = 0;
            private int _productId = 0;

            /// <summary>
            ///     Creates a new <see cref="ProductCartFilters"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public ProductCartFilters(ProductCartEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryFilter("PRC_ID", _id);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public decimal Price
            {
                get => _price;
                set
                {
                    _price = value;
                    Entity.AddQueryFilter("PRC_PRICE", _price);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public int Quantity
            {
                get => _quantity;
                set
                {
                    _quantity = value;
                    Entity.AddQueryFilter("PRC_QUANTITY", _quantity);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public decimal Total
            {
                get => _total;
                set
                {
                    _total = value;
                    Entity.AddQueryFilter("PRC_TOTAL", _total);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public DateTime AddedAt
            {
                get => _addedAt;
                set
                {
                    _addedAt = value;
                    Entity.AddQueryFilter("PRC_ADDED_AT", _addedAt);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public int CartId
            {
                get => _cartId;
                set
                {
                    _cartId = value;
                    Entity.AddQueryFilter("CRT_ID", _cartId);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
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

        /// <summary>
        ///     Contains a mapping between SQL attributes for the "ProductCart" database object
        /// and the <see cref="ProductCartEntity"/> class, utilized to set values to the attributes
        /// in the same database object.
        /// </summary>
        public class ProductCartValues
        {
            /// <summary>
            ///     The parent <see cref="ProductCartEntity"/>.
            /// </summary>
            public ProductCartEntity Entity { set; get; }

            ///     Internal fields of the class.
            private int _id = 0;
            private decimal _price = Decimal.Zero;
            private int _quantity = 0;
            private decimal _total = decimal.Zero;
            private DateTime _addedAt = DateTime.Now;
            private int _cartId = 0;
            private int _productId = 0;

            /// <summary>
            ///     Creates a new <see cref="ProductCartValues"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public ProductCartValues(ProductCartEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddFieldValue("PRC_ID", _id);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public decimal Price
            {
                get => _price;
                set
                {
                    _price = value;
                    Entity.AddFieldValue("PRC_PRICE", _price);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public int Quantity
            {
                get => _quantity;
                set
                {
                    _quantity = value;
                    Entity.AddFieldValue("PRC_QUANTITY", _quantity);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public decimal Total
            {
                get => _total;
                set
                {
                    _total = value;
                    Entity.AddFieldValue("PRC_TOTAL", _total);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public DateTime AddedAt
            {
                get => _addedAt;
                set
                {
                    _addedAt = value;
                    Entity.AddFieldValue("PRC_ADDED_AT", _addedAt);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public int CartId
            {
                get => _cartId;
                set
                {
                    _cartId = value;
                    Entity.AddFieldValue("CRT_ID", _cartId);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductCartEntity"/>
            /// attribute.
            /// </summary>
            public int ProductId
            {
                get => _productId;
                set
                {
                    _productId = value;
                    Entity.AddFieldValue("PRD_ID", _productId);
                }
            }
        }
    }
}
