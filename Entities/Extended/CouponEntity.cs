using shopping_api.Entities.Default;
using Shopping_API.Entities.Attributes;

namespace shopping_api.Entities.Extended
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

        /// <summary>
        ///     Defines an object that contains the relations between the <see cref="CouponEntity"/>
        /// and other entities.
        /// </summary>
        public class CouponRelations
        {
            /// <summary>
            ///     The parent <see cref="CouponEntity"/>.
            /// </summary>
            public CouponEntity Entity { set; get; }

            /// <summary>
            ///     Creates a new <see cref="CouponRelations"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public CouponRelations(CouponEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     Adds (binds) an entity to the given <see cref="CouponEntity"/>.
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

        /// <summary>
        ///     Contains a mapping between SQL attributes for the "Coupon" database object
        /// and the <see cref="CouponEntity"/> class, utilized to set values to the attributes
        /// in the same database object.
        /// </summary>
        public class CouponValues
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
            ///     Creates a new <see cref="CouponValues"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public CouponValues(CouponEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="CouponEntity"/>
            /// attribute.
            /// </summary>
            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.FieldValues.Add(new EntityField("CPN_ID", _id));
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="CouponEntity"/>
            /// attribute.
            /// </summary>
            public string Code
            {
                get => _code;
                set
                {
                    _code = value;
                    Entity.FieldValues.Add(new EntityField("CPN_CODE", _code));
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="CouponEntity"/>
            /// attribute.
            /// </summary>
            public string Description
            {
                get => _description;
                set
                {
                    _description = value;
                    Entity.FieldValues.Add(new EntityField("CPN_DESCRIPTION", _description));
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="CouponEntity"/>
            /// attribute.
            /// </summary>
            public decimal Discount
            {
                get => _discount;
                set
                {
                    _discount = value;
                    Entity.FieldValues.Add(new EntityField("CPN_DISCOUNT", _discount));
                }
            }
        }
    }
}
