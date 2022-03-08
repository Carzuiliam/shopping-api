namespace Shopping_API.Models
{
    /// <summary>
    ///     Defines a object which represents a product in a shopping cart.
    /// </summary>
    public class ProductCart
    {
        /// <summary>
        ///     Contains the ID of the product in a shopping cart.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Contains the ID of the cart where the product is stored.
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        ///     Contains the ID of the original product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        ///     Contains the price of the product in a shopping cart.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Contains the quantity of the product in a shopping cart.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        ///     Contains the total of the product in a shopping cart.
        /// </summary>
        public decimal Total{ get; set; }

        /// <summary>
        ///     Contains the date/time when the product were added to the cart.
        /// </summary>
        public DateTime AddedAt { get; set; }

        /// <summary>
        ///     Contains the corresponding product of the product in the cart.
        /// </summary>
        public Product? Product { get; set; }

        /// <summary>
        ///     Creates a new instance of a <see cref="ProductCart"/> object.
        /// </summary>
        public ProductCart()
        {
            Id = 0;
            CartId = 0;
            ProductId = 0;
            Price = decimal.Zero;
            Quantity = 0;
            Total = decimal.Zero;
            AddedAt = DateTime.Now;
            Product = null;
        }
    }
}
