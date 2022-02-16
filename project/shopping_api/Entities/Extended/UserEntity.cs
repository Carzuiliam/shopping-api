using shopping_api.Entities.Default;
using Shopping_API.Entities.Attributes;

namespace shopping_api.Entities.Extended
{
    /// <summary>
    ///     Defines a custom entity, which represents the SQL object "User" from the SQL
    /// database.
    /// </summary>
    public class UserEntity : BaseEntity
    {
        /// <summary>
        ///     Contains the relations between the given <see cref="UserEntity"/>
        /// entity and other entities.
        /// </summary>
        public UserRelations Relations { get; set; }

        /// <summary>
        ///     Contains the filters of the given <see cref="UserEntity"/>.
        /// </summary>
        public UserFilters Filters { get; set; }

        /// <summary>
        ///     Contains the values for the given <see cref="UserEntity"/>.
        /// </summary>
        public UserValues Values { get; set; }

        /// <summary>
        ///     Creates a new <see cref="UserEntity"/> object (an extension of the
        /// <see cref="BaseEntity"/> object).
        /// </summary>
        public UserEntity() : base("TB_USER", "USR_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        /// <summary>
        ///     Defines an object that contains the relations between the <see cref="UserEntity"/>
        /// and other entities.
        /// </summary>
        public class UserRelations
        {
            /// <summary>
            ///     The parent <see cref="UserEntity"/>.
            /// </summary>
            public UserEntity Entity { set; get; }

            /// <summary>
            ///     Creates a new <see cref="UserRelations"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public UserRelations(UserEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     Adds (binds) an entity to the given <see cref="UserEntity"/>.
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
        ///     Contains a mapping between SQL attributes for the "User" database object
        /// and the <see cref="UserEntity"/> class, which is utilized to filter values
        /// from the same database object.
        /// </summary>
        public class UserFilters
        {
            /// <summary>
            ///     The parent <see cref="UserEntity"/>.
            /// </summary>
            public UserEntity Entity { set; get; }

            ///     Internal fields of the class.
            private int _id = 0;
            private string _username = "";
            private string _name = "";

            /// <summary>
            ///     Creates a new <see cref="UserFilters"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public UserFilters(UserEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="UserEntity"/>
            /// attribute.
            /// </summary>
            public int Id
            {
                get => _id;
                set 
                {
                    _id = value;
                    Entity.AddQueryFilter("USR_ID", _id);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="UserEntity"/>
            /// attribute.
            /// </summary>
            public string Username
            {
                get => _username;
                set
                {
                    _username = value;
                    Entity.AddQueryFilter("USR_USERNAME", _username);
                }
            }

            /// <summary>
            ///     A field that contains a filter for the corresponding <see cref="UserEntity"/>
            /// attribute.
            /// </summary>
            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.AddQueryFilter("USR_NAME", _name);
                }
            }            
        }

        /// <summary>
        ///     Contains a mapping between SQL attributes for the "User" database object
        /// and the <see cref="UserEntity"/> class, utilized to set values to the attributes
        /// in the same database object.
        /// </summary>
        public class UserValues
        {
            /// <summary>
            ///     The parent <see cref="UserEntity"/>.
            /// </summary>
            public UserEntity Entity { set; get; }

            ///     Internal fields of the class.
            private int _id = 0;
            private string _username = "";
            private string _name = "";

            /// <summary>
            ///     Creates a new <see cref="UserValues"/> object.
            /// </summary>
            /// 
            /// <param name="_entity">The parent entity.</param>
            public UserValues(UserEntity _entity)
            {
                Entity = _entity;
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="UserEntity"/>
            /// attribute.
            /// </summary>
            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddFieldValue("USR_ID", _id);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="UserEntity"/>
            /// attribute.
            /// </summary>
            public string Username
            {
                get => _username;
                set
                {
                    _username = value;
                    Entity.AddFieldValue("USR_USERNAME", _username);
                }
            }

            /// <summary>
            ///     A field that contains a new value for the corresponding <see cref="UserEntity"/>
            /// attribute.
            /// </summary>
            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.AddFieldValue("USR_NAME", _name);
                }
            }            
        }
    }
}
