using shopping_api.Entities.Default;
using Shopping_API.Entities.Filters;

namespace shopping_api.Entities.Extended
{
    public class BrandEntity : BaseEntity
    {
        public BrandRelations Relations { get; set; }

        public BrandFilters Filters { get; set; }

        public BrandValues Values { get; set; }

        public BrandEntity() : base("TB_BRAND", "BRN_ID")
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        public class BrandRelations
        {
            public BrandEntity Entity { set; get; }

            public BrandRelations(BrandEntity _entity)
            {
                Entity = _entity;
            }

            public void Bind(BaseEntity _entity, EntityFilter.RelationType _relationType)
            {
                Entity.AddEntityFilter(_entity, _relationType);
            }
        }

        public class BrandFilters
        {
            public BrandEntity Entity { set; get; }

            private int _id = 0;
            private string _code = "";
            private string _name = "";

            public BrandFilters(BrandEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryFilter("BRN_ID", _id);
                }
            }

            public string Code
            {
                get => _code;
                set
                {
                    _code = value;
                    Entity.AddQueryFilter("BRN_CODE", _code);
                }
            }

            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.AddQueryFilter("BRN_NAME", _name);
                }
            }
        }

        public class BrandValues
        {
            public BrandEntity Entity { set; get; }

            private int _id = 0;
            private string _code = "";
            private string _name = "";

            public BrandValues(BrandEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryValue("BRN_ID", _id);
                }
            }

            public string Code
            {
                get => _code;
                set
                {
                    _code = value;
                    Entity.AddQueryValue("BRN_CODE", _code);
                }
            }

            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.AddQueryValue("BRN_NAME", _name);
                }
            }
        }
    }
}
