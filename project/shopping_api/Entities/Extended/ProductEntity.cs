using shopping_api.Entities.Default;
using Shopping_API.Entities.Attributes;

namespace shopping_api.Entities.Extended
{
    /// <summary>
    ///     Defines a custom entity, which represents the SQL object "Product" from the SQL
    /// database.
    /// </summary>
    public class ProductEntity : BaseEntity
    {
        /// <summary>
        ///     Contains the relations between the given <see cref="ProductEntity"/>
        /// entity and other entities.
        /// </summary>
        public ProductRelations Relations { get; set; }

        /// <summary>
        ///     Contains the filters of the given <see cref="ProductEntity"/>.
        /// </summary>
        public ProductFilters Filters { get; set; }

        /// <summary>
        ///     Contains the values for the given <see cref="ProductEntity"/>.
        /// </summary>
        public ProductValues Values { get; set; }

        /// <summary>
        ///     Creates a new <see cref="ProductEntity"/> object (an extension of the
        /// <see cref="BaseEntity"/> object).
        /// </summary>
        public ProductEntity() : base("TB_PRODUCT", "PRD_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        /// <summary>
        ///     Defines an object that contains the relations between the <see cref="ProductEntity"/>
        /// and other entities.
        /// </summary>
        public class ProductRelations
        {
            /// <summary>
            ///     The parent <see cref="ProductEntity"/>.
            /// </summary>
            public ProductEntity Entity { set; get; }

            /// <summary>
            ///     Creates a new <see cref="ProductRelations"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public ProductRelations(ProductEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     Adds (binds) an entity to the given <see cref="ProductEntity"/>.
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
        ///     Contains a mapping between SQL attributes for the "Product" database object
        /// and the <see cref="ProductEntity"/> class, which is utilized to filter values
        /// from the same database object.
        /// </summary>
        public class ProductFilters
        {
            /// <summary>
            ///     The parent <see cref="ProductEntity"/>.
            /// </summary>
            public ProductEntity Entity { set; get; }

            ///     Internal fields of the class.
            private int _id = 0;
            private string _code = "";
            private string _name = "";
            private decimal _price = decimal.Zero;
            private int _stock = 0;
            private int _ProductId = 0;
            private int _departmentId = 0;

            /// <summary>
            ///     Creates a new <see cref="ProductFilters"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public ProductFilters(ProductEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryFilter("PRD_ID", _id);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public string Code
            {
                get => _code;
                set
                {
                    _code = value;
                    Entity.AddQueryFilter("PRD_CODE", _code);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.AddQueryFilter("PRD_NAME", _name);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public decimal Price
            {
                get => _price;
                set
                {
                    _price = value;
                    Entity.AddQueryFilter("PRD_PRICE", _price);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public int Stock
            {
                get => _stock;
                set
                {
                    _stock = value;
                    Entity.AddQueryFilter("PRD_STOCK", _stock);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public int ProductId
            {
                get => _ProductId;
                set
                {
                    _ProductId = value;
                    Entity.AddQueryFilter("BRN_ID", _ProductId);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public int DepartmentId
            {
                get => _departmentId;
                set
                {
                    _departmentId = value;
                    Entity.AddQueryFilter("DPR_ID", _departmentId);
                }
            }
        }

        /// <summary>
        ///     Contains a mapping between SQL attributes for the "Product" database object
        /// and the <see cref="ProductEntity"/> class, utilized to set values to the attributes
        /// in the same database object.
        /// </summary>
        public class ProductValues
        {
            /// <summary>
            ///     The parent <see cref="ProductEntity"/>.
            /// </summary>
            public ProductEntity Entity { set; get; }

            ///     Internal fields of the class.
            private int _id = 0;
            private string _code = "";
            private string _name = "";
            private decimal _price = decimal.Zero;
            private int _stock = 0;
            private int _ProductId = 0;
            private int _departmentId = 0;

            /// <summary>
            ///     Creates a new <see cref="ProductValues"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public ProductValues(ProductEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddFieldValue("PRD_ID", _id);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public string Code
            {
                get => _code;
                set
                {
                    _code = value;
                    Entity.AddFieldValue("PRD_CODE", _code);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.AddFieldValue("PRD_NAME", _name);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public decimal Price
            {
                get => _price;
                set
                {
                    _price = value;
                    Entity.AddFieldValue("PRD_PRICE", _price);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public int Stock
            {
                get => _stock;
                set
                {
                    _stock = value;
                    Entity.AddFieldValue("PRD_STOCK", _stock);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public int ProductId
            {
                get => _ProductId;
                set
                {
                    _ProductId = value;
                    Entity.AddFieldValue("BRN_ID", _ProductId);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="ProductEntity"/>
            /// attribute.
            /// </summary>
            public int DepartmentId
            {
                get => _departmentId;
                set
                {
                    _stock = value;
                    Entity.AddFieldValue("DPR_ID", _departmentId);
                }
            }
        }
    }
}
