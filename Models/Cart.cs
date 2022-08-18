namespace Shopping_API.Models
{
    /// <summary>
    ///     Defines a object which represents a shopping cart.
    /// </summary>
    public class Cart
    {
        /// <summary>
        ///     Contains the ID of the cart.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        ///     Contains the subtotal value of the cart.
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        ///     Contains the discount value of the cart.
        /// </summary>
        public decimal Discount{ get; set; }


        /// <summary>
        ///     Contains the shipping value of the cart.
        /// </summary>
        public decimal Shipping { get; set; }

        /// <summary>
        ///     Contains the total value of the cart.
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        ///     Contains the date/time when the cart has been created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     Contains the user that is the owner for the cart.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     Contains a discount coupon for the cart (if any).
        /// </summary>
        public Coupon? Coupon { get; set; }

        /// <summary>
        ///     Contains the list of the products in the cart.
        /// </summary>
        public List<ProductCart> ProductCart { get; set; }

        /// <summary>
        ///     Creates a new instance of a <see cref="Cart"/> object.
        /// </summary>
        public Cart()
        {
            Id = 0;
            Subtotal = decimal.Zero;
            Discount = decimal.Zero;
            Shipping = decimal.Zero;
            Total = decimal.Zero;
            CreatedAt = DateTime.Now;
            User = new();
            Coupon = null;
            ProductCart = new();
        }
    }
}
