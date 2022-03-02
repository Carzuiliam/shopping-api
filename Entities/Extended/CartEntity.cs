using Microsoft.Data.Sqlite;
using shopping_api.Entities.Default;
using shopping_api.Models;
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
                Entity.BindedEntities.Add(new EntityRelation(_entity, _relationType));
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
                    Entity.FieldValues.Add(new EntityField("CRT_ID", _id));
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
                    Entity.FieldValues.Add(new EntityField("CRT_SUBTOTAL", _subtotal));
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
                    Entity.FieldValues.Add(new EntityField("CRT_DISCOUNT", _discount));
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
                    Entity.FieldValues.Add(new EntityField("CRT_SHIPPING", _shipping));
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
                    Entity.FieldValues.Add(new EntityField("CRT_TOTAL", _total));
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
                    Entity.FieldValues.Add(new EntityField("CRT_CREATED_AT", _createdAt));
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
                    Entity.FieldValues.Add(new EntityField("USR_ID", _userId));
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
                    Entity.FieldValues.Add(new EntityField("CPN_ID", _couponId));
                }
            }
        }

        /// <summary>
        ///     Selects a list of "Cart" objects from the database, returning them as carts.
        /// Any filter applied before the call of this method will affect the returned results.
        /// </summary>
        /// 
        /// <returns>
        ///     A list with a set of carts from the database.
        /// </returns>
        public List<Cart> Select()
        {
            List<Cart> carts = new();

            using (var db = new SqliteConnection(CONNECTION_STRING))
            {
                db.Open();

                SqliteCommand command = db.CreateCommand();
                command.CommandText = IsBinded ? SQLJoin() : SQLSelect();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cart cart = new()
                        {
                            Id = reader.GetInt32(0),
                            Subtotal = reader.GetDecimal(1),
                            Discount = reader.GetDecimal(2),
                            Shipping = reader.GetDecimal(3),
                            Total = reader.GetDecimal(4),
                            CreatedAt = reader.GetDateTime(5)
                        };

                        if (IsBinded)
                        {
                            cart.User = new()
                            {
                                Id = reader.GetInt32(8),
                                Username = reader.GetString(9),
                                Name = reader.GetString(10)
                            };

                            if (!reader.IsDBNull(11))
                            {
                                cart.Coupon = new()
                                {
                                    Id = reader.GetInt32(11),
                                    Code = reader.GetString(12),
                                    Description = reader.GetString(13),
                                    Discount = reader.GetDecimal(14)
                                };
                            };
                        }

                        carts.Add(cart);
                    }
                }
            }

            ClearParameters();

            return carts;
        }
    }
}
