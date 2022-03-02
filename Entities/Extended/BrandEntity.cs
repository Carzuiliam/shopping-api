using Microsoft.Data.Sqlite;
using shopping_api.Entities.Default;
using shopping_api.Models;
using Shopping_API.Entities.Attributes;

namespace shopping_api.Entities.Extended
{
    /// <summary>
    ///     Defines a custom entity, which represents the SQL object "Brand" from the SQL
    /// database.
    /// </summary>
    public class BrandEntity : BaseEntity
    {
        /// <summary>
        ///     Contains the relations between the given <see cref="BrandEntity"/>
        /// entity and other entities.
        /// </summary>
        public BrandRelations Relations { get; set; }

        /// <summary>
        ///     Contains the filters of the given <see cref="BrandEntity"/>.
        /// </summary>
        public BrandFilters Filters { get; set; }

        /// <summary>
        ///     Contains the values for the given <see cref="BrandEntity"/>.
        /// </summary>
        public BrandValues Values { get; set; }

        /// <summary>
        ///     Creates a new <see cref="BrandEntity"/> object (an extension of the
        /// <see cref="BaseEntity"/> object).
        /// </summary>
        public BrandEntity() : base("TB_BRAND", "BRN_ID")
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        /// <summary>
        ///     Defines an object that contains the relations between the <see cref="BrandEntity"/>
        /// and other entities.
        /// </summary>
        public class BrandRelations
        {
            /// <summary>
            ///     The parent <see cref="BrandEntity"/>.
            /// </summary>
            public BrandEntity Entity { set; get; }

            /// <summary>
            ///     Creates a new <see cref="BrandRelations"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public BrandRelations(BrandEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     Adds (binds) an entity to the given <see cref="BrandEntity"/>.
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
        ///     Contains a mapping between SQL attributes for the "Brand" database object
        /// and the <see cref="BrandEntity"/> class, which is utilized to filter values
        /// from the same database object.
        /// </summary>
        public class BrandFilters
        {
            /// <summary>
            ///     The parent <see cref="BrandEntity"/>.
            /// </summary>
            public BrandEntity Entity { set; get; }

            ///     Internal fields of the class.
            private int _id = 0;
            private string _code = "";
            private string _name = "";

            /// <summary>
            ///     Creates a new <see cref="BrandFilters"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public BrandFilters(BrandEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="BrandEntity"/>
            /// attribute.
            /// </summary>
            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.QueryFilters.Add(new EntityField("BRN_ID", _id));
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="BrandEntity"/>
            /// attribute.
            /// </summary>
            public string Code
            {
                get => _code;
                set
                {
                    _code = value;
                    Entity.QueryFilters.Add(new EntityField("BRN_CODE", _code));
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="BrandEntity"/>
            /// attribute.
            /// </summary>
            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.QueryFilters.Add(new EntityField("BRN_NAME", _name));
                }
            }
        }

        /// <summary>
        ///     Contains a mapping between SQL attributes for the "Brand" database object
        /// and the <see cref="BrandEntity"/> class, utilized to set values to the attributes
        /// in the same database object.
        /// </summary>
        public class BrandValues
        {
            /// <summary>
            ///     The parent <see cref="BrandEntity"/>.
            /// </summary>
            public BrandEntity Entity { set; get; }

            ///     Internal fields of the class.
            private int _id = 0;
            private string _code = "";
            private string _name = "";

            /// <summary>
            ///     Creates a new <see cref="BrandValues"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public BrandValues(BrandEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="BrandEntity"/>
            /// attribute.
            /// </summary>
            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.FieldValues.Add(new EntityField("BRN_ID", _id));
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="BrandEntity"/>
            /// attribute.
            /// </summary>
            public string Code
            {
                get => _code;
                set
                {
                    _code = value;
                    Entity.FieldValues.Add(new EntityField("BRN_CODE", _code));
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="BrandEntity"/>
            /// attribute.
            /// </summary>
            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.FieldValues.Add(new EntityField("BRN_NAME", _name));
                }
            }
        }
    
        /// <summary>
        ///     Selects a list of "Brand" objects from the database, returning them as brands.
        /// Any filter applied before the call of this method will affect the returned results.
        /// </summary>
        /// 
        /// <returns>
        ///     A list with a set of brands from the database.
        /// </returns>
        public List<Brand> Select()
        {
            List<Brand> brands = new();

            using (var db = new SqliteConnection(CONNECTION_STRING))
            {
                db.Open();

                SqliteCommand command = db.CreateCommand();
                command.CommandText = SQLSelect();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Brand brand = new()
                        {
                            Id = reader.GetInt32(0),
                            Code = reader.GetString(1),
                            Name = reader.GetString(2)
                        };

                        brands.Add(brand);
                    }
                }
            }

            ClearParameters();

            return brands;
        }
    }
}
