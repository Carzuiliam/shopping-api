using shopping_api.Entities.Default;
using Shopping_API.Entities.Filters;

namespace shopping_api.Entities.Extended
{
    public class ProductEntity : BaseEntity
    {
        public ProductRelations Relations { get; set; }

        public ProductFilters Filters { get; set; }

        public ProductValues Values { get; set; }

        public ProductEntity() : base("TB_PRODUCT", "PRD_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        public class ProductRelations
        {
            public ProductEntity Entity { set; get; }

            public ProductRelations(ProductEntity _entity)
            {
                Entity = _entity;
            }

            public void Bind(BaseEntity _entity, EntityFilter.RelationType _relationType)
            {
                Entity.AddEntityFilter(_entity, _relationType);
            }

            internal void Bind(BrandEntity brandEntity)
            {
                throw new NotImplementedException();
            }
        }

        public class ProductFilters
        {
            public ProductEntity Entity { set; get; }

            private int _id = 0;
            private string _code = "";
            private string _name = "";
            private decimal _price = decimal.Zero;
            private int _stock = 0;
            private int _brandId = 0;
            private int _departmentId = 0;

            public ProductFilters(ProductEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryFilter("PRD_ID", _id);
                }
            }

            public string Code
            {
                get => _code;
                set
                {
                    _code = value;
                    Entity.AddQueryFilter("PRD_CODE", _code);
                }
            }

            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.AddQueryFilter("PRD_NAME", _name);
                }
            }

            public decimal Price
            {
                get => _price;
                set
                {
                    _price = value;
                    Entity.AddQueryFilter("PRD_PRICE", _price);
                }
            }

            public int Stock
            {
                get => _stock;
                set
                {
                    _stock = value;
                    Entity.AddQueryFilter("PRD_STOCK", _stock);
                }
            }

            public int BrandId
            {
                get => _brandId;
                set
                {
                    _brandId = value;
                    Entity.AddQueryFilter("BRN_ID", _brandId);
                }
            }

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

        public class ProductValues
        {
            public ProductEntity Entity { set; get; }

            private int _id = 0;
            private string _code = "";
            private string _name = "";
            private decimal _price = decimal.Zero;
            private int _stock = 0;
            private int _brandId = 0;
            private int _departmentId = 0;

            public ProductValues(ProductEntity _entity)
            {
                Entity = _entity;
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    Entity.AddQueryValue("PRD_ID", _id);
                }
            }

            public string Code
            {
                get => _code;
                set
                {
                    _code = value;
                    Entity.AddQueryValue("PRD_CODE", _code);
                }
            }

            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    Entity.AddQueryValue("PRD_NAME", _name);
                }
            }

            public decimal Price
            {
                get => _price;
                set
                {
                    _price = value;
                    Entity.AddQueryValue("PRD_PRICE", _price);
                }
            }

            public int Stock
            {
                get => _stock;
                set
                {
                    _stock = value;
                    Entity.AddQueryValue("PRD_STOCK", _stock);
                }
            }

            public int BrandId
            {
                get => _brandId;
                set
                {
                    _brandId = value;
                    Entity.AddQueryValue("BRN_ID", _brandId);
                }
            }

            public int DepartmentId
            {
                get => _departmentId;
                set
                {
                    _stock = value;
                    Entity.AddQueryValue("DPR_ID", _departmentId);
                }
            }
        }
    }
}
