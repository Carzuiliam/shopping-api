using Shopping_API.Models;
using Shopping_API.Entities.Base;
using Shopping_API.Entities.Relations;
using Shopping_API.Entities.Filters;
using Shopping_API.Entities.Values;
using Shopping_API.Entities.Connection;

namespace Shopping_API.Entities.Extended
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
        ///     Selects a list of "User" objects from the database using an <see cref="EntityDB"/>
        /// object, returning them as users. Any filter applied before the call of this method
        /// will affect the returned results.
        /// </summary>
        /// 
        /// <param name="_entityDB">A target <see cref="EntityDB"/> object to perform the query.</param>
        /// 
        /// <returns>
        ///     A list with a set of users from the database.
        /// </returns>
        public List<User> Select(EntityDB _entityDB)
        {
            List<User> users = new();

            using (var reader = _entityDB.Query(SQLSelect()))
            {
                while (reader.Read())
                {
                    User user = new()
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Name = reader.GetString(2)
                    };

                    users.Add(user);
                }
            }

            ClearParameters();

            return users;
        }
    }
}
