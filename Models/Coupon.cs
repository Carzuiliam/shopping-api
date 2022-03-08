namespace Shopping_API.Models
{
    /// <summary>
    ///     Defines a object which represents a discount coupon.
    /// </summary>
    public class Coupon
    {
        /// <summary>
        ///     Contains the ID of the coupon.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Contains the code of the coupon.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Contains the description of the coupon.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Contains the discount value of the coupon.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        ///     Creates a new instance of a <see cref="Coupon"/> object.
        /// </summary>
        public Coupon()
        {
            Id = 0;
            Code = string.Empty;
            Description = string.Empty;
            Discount = decimal.Zero;
        }
    }    
}
