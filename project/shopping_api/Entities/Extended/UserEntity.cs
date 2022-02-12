using shopping_api.Entities.Default;
using Shopping_API.Entities.Filters;

namespace shopping_api.Entities.Extended
{
    public class UserEntity : BaseEntity
    {
        public UserRelations Relations { get; set; }

        public UserFilters Filters { get; set; }

        public UserValues Values { get; set; }

        public UserEntity() : base("TB_USER", "USR_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        public class UserRelations
        {
            public UserEntity Entity { set; get; }

            public UserRelations(UserEntity _entity)
            {
                Entity = _entity;
            }

            public void Bind(BaseEntity _entity, EntityFilter.RelationType _relationType)
            {
                Entity.AddEntityFilter(_entity, _relationType);
            }
        }

        public class UserFilters
        {
            public UserEntity Entity { set; get; }

            private int _id = 0;
            private string _username = "";
            private string _name = "";

            public UserFilters(UserEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set 
                {
                    _id = value;
                    Entity.AddQueryFilter("USR_ID", _id);
                }
            }

            public string Username
            {
                get => _username;
                set
                {
                    _username = value;
                    Entity.AddQueryFilter("USR_USERNAME", _username);
                }
            }

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

        public class UserValues
        {
            public UserEntity Entity { set; get; }

            private int _id = 0;
            private string _username = "";
            private string _name = "";

            public UserValues(UserEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryValue("USR_ID", _id);
                }
            }

            public string Username
            {
                get => _username;
                set
                {
                    _username = value;
                    Entity.AddQueryValue("USR_USERNAME", _username);
                }
            }

            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.AddQueryValue("USR_NAME", _name);
                }
            }            
        }
    }
}
