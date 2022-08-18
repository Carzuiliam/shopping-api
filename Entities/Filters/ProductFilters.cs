using Shopping_API.Entities.Base;
using Shopping_API.Entities.Extended;

namespace Shopping_API.Entities.Filters
{
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
        private int _productId = 0;
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
                Entity.QueryFilters.Add(new EntityField("PRD_ID", _id));
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
                Entity.QueryFilters.Add(new EntityField("PRD_CODE", _code));
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
                Entity.QueryFilters.Add(new EntityField("PRD_NAME", _name));
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
                Entity.QueryFilters.Add(new EntityField("PRD_PRICE", _price));
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
                Entity.QueryFilters.Add(new EntityField("PRD_STOCK", _stock));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="ProductEntity"/>
        /// attribute.
        /// </summary>
        public int ProductId
        {
            get => _productId;
            set
            {
                _productId = value;
                Entity.QueryFilters.Add(new EntityField("BRN_ID", _productId));
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
                Entity.QueryFilters.Add(new EntityField("DPR_ID", _departmentId));
            }
        }
    }
}
