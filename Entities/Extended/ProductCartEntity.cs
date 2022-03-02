using Microsoft.Data.Sqlite;
using shopping_api.Entities.Default;
using shopping_api.Models;
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
                Entity.BindedEntities.Add(new EntityRelation(_entity, _relationType));
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
                    Entity.QueryFilters.Add(new EntityField("PRC_ID", _id));
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
                    Entity.QueryFilters.Add(new EntityField("PRC_PRICE", _price));
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
                    Entity.QueryFilters.Add(new EntityField("PRC_QUANTITY", _quantity));
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
                    Entity.QueryFilters.Add(new EntityField("PRC_TOTAL", _total));
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
                    Entity.QueryFilters.Add(new EntityField("PRC_ADDED_AT", _addedAt));
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
                    Entity.QueryFilters.Add(new EntityField("CRT_ID", _cartId));
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
                    Entity.QueryFilters.Add(new EntityField("PRD_ID", _productId));
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
                    Entity.FieldValues.Add(new EntityField("PRC_ID", _id));
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
                    Entity.FieldValues.Add(new EntityField("PRC_PRICE", _price));
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
                    Entity.FieldValues.Add(new EntityField("PRC_QUANTITY", _quantity));
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
                    Entity.FieldValues.Add(new EntityField("PRC_TOTAL", _total));
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
                    Entity.FieldValues.Add(new EntityField("PRC_ADDED_AT", _addedAt));
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
                    Entity.FieldValues.Add(new EntityField("CRT_ID", _cartId));
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
                    Entity.FieldValues.Add(new EntityField("PRD_ID", _productId));
                }
            }
        }

        /// <summary>
        ///     Selects a list of "ProductCart" objects from the database, returning them as
        /// products in a cart. Any filter applied before the call of this method will affect
        /// the returned results.
        /// </summary>
        /// 
        /// <returns>
        ///     A list with a set of products in a cart from the database.
        /// </returns>
        public List<ProductCart> Select()
        {
            List<ProductCart> productsCart = new();

            using (var db = new SqliteConnection(CONNECTION_STRING))
            {
                db.Open();

                SqliteCommand command = db.CreateCommand();
                command.CommandText = IsBinded ? SQLJoin() : SQLSelect();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductCart productCart = new()
                        {
                            Id = reader.GetInt32(0),
                            Price = reader.GetDecimal(1),
                            Quantity = reader.GetInt32(2),
                            Total = reader.GetDecimal(3),
                            AddedAt = reader.GetDateTime(4),
                            CartId = reader.GetInt32(5),
                            ProductId = reader.GetInt32(6)
                        };

                        if (IsBinded)
                        {
                            productCart.Product = new()
                            {
                                Id = reader.GetInt32(7),
                                Code = reader.GetString(8),
                                Name = reader.GetString(9),
                                Price = reader.GetDecimal(10),
                                Stock = reader.GetInt32(11)
                            };
                        }

                        productsCart.Add(productCart);
                    }
                }
            }

            ClearParameters();

            return productsCart;
        }
    }
}
