namespace Shopping_API.Models
{
    /// <summary>
    ///     Defines a object which represents a product.
    /// </summary>
    public class Product
    {
        /// <summary>
        ///     Contains the ID of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Contains the code of the product.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Contains the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Contains the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Contains the quantity in stock of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        ///     Contains the brand for the product (if necessary).
        /// </summary>
        public Brand? Brand { get; set; }

        /// <summary>
        ///     Contains the department of the product (if necessary).
        /// </summary>
        public Department? Department { get; set; }

        /// <summary>
        ///     Creates a new instance of a <see cref="Product"/> object.
        /// </summary>
        public Product()
        {
            Id = 0;
            Code = string.Empty;
            Name = string.Empty;
            Price = decimal.Zero;
            Stock = 0;
            Brand = null;
            Department = null;
        }
    }
}
