using Shopping_API.Entities.Base;
using Shopping_API.Entities.Extended;

namespace Shopping_API.Entities.Values
{
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
                Entity.FieldValues.Add(new EntityField("USR_ID", _id));
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
                Entity.FieldValues.Add(new EntityField("USR_USERNAME", _username));
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
                Entity.FieldValues.Add(new EntityField("USR_NAME", _name));
            }
        }
    }
}
