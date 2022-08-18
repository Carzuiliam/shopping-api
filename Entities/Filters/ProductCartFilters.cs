using Shopping_API.Entities.Base;
using Shopping_API.Entities.Extended;

namespace Shopping_API.Entities.Filters
{
    /// <summary>
    ///     Contains a mapping between SQL attributes for the "ProductCart" database object
    /// and the <see cref="ProductCartEntity"/> class, which is utilized to filter values
    /// from the same database object.
    /// </summary>
    public class ProductCartFilters
    {
        /// <summary>
        ///     The parent <see cref="ProductCartEntity"/>.
        /// </summary>
        public ProductCartEntity Entity { set; get; }

        ///     Internal fields of the class.
        private int _id = 0;
        private decimal _price = decimal.Zero;
        private int _quantity = 0;
        private decimal _total = decimal.Zero;
        private DateTime _addedAt = DateTime.Now;
        private int _cartId = 0;
        private int _productId = 0;

        /// <summary>
        ///     Creates a new <see cref="ProductCartFilters"/> object.
        /// </summary>
        /// 
        /// <param name="_entity">The parent entity.</param>
        public ProductCartFilters(ProductCartEntity _entity)
        {
            Entity = _entity;
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
        /// attribute.
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                Entity.QueryFilters.Add(new EntityField("PRC_ID", _id));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
        /// attribute.
        /// </summary>
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                Entity.QueryFilters.Add(new EntityField("PRC_PRICE", _price));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
        /// attribute.
        /// </summary>
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                Entity.QueryFilters.Add(new EntityField("PRC_QUANTITY", _quantity));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
        /// attribute.
        /// </summary>
        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                Entity.QueryFilters.Add(new EntityField("PRC_TOTAL", _total));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
        /// attribute.
        /// </summary>
        public DateTime AddedAt
        {
            get => _addedAt;
            set
            {
                _addedAt = value;
                Entity.QueryFilters.Add(new EntityField("PRC_ADDED_AT", _addedAt));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
        /// attribute.
        /// </summary>
        public int CartId
        {
            get => _cartId;
            set
            {
                _cartId = value;
                Entity.QueryFilters.Add(new EntityField("CRT_ID", _cartId));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="ProductCartEntity"/>
        /// attribute.
        /// </summary>
        public int ProductId
        {
            get => _productId;
            set
            {
                _productId = value;
                Entity.QueryFilters.Add(new EntityField("PRD_ID", _productId));
            }
        }
    }
}
