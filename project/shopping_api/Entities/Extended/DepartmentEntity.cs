using shopping_api.Entities.Default;
using Shopping_API.Entities.Filters;

namespace shopping_api.Entities.Extended
{
    public class DepartmentEntity : BaseEntity
    {
        public DepartmentRelations Relations { get; set; }

        public DepartmentFilters Filters { get; set; }

        public DepartmentValues Values { get; set; }

        public DepartmentEntity() : base("TB_DEPARTMENT", "DPR_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        public class DepartmentRelations
        {
            public DepartmentEntity Entity { set; get; }

            public DepartmentRelations(DepartmentEntity _entity)
            {
                Entity = _entity;
            }

            public void Bind(BaseEntity _entity, EntityFilter.RelationType _relationType)
            {
                Entity.AddEntityFilter(_entity, _relationType);
            }
        }

        public class DepartmentFilters
        {
            public DepartmentEntity Entity { set; get; }

            private int _id = 0;
            private string _name = "";

            public DepartmentFilters(DepartmentEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryFilter("DPR_ID", _id);
                }
            }

            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.AddQueryFilter("DPR_NAME", _name);
                }
            }
        }

        public class DepartmentValues
        {
            public DepartmentEntity Entity { set; get; }

            private int _id = 0;
            private string _name = "";

            public DepartmentValues(DepartmentEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryValue("DPR_ID", _id);
                }
            }

            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.AddQueryValue("DPR_NAME", _name);
                }
            }
        }
    }
}
