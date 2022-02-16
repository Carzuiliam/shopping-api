namespace shopping_api.Models
{
    /// <summary>
    ///     Defines a object which represents a brand.
    /// </summary>
    public class Brand
    {
        /// <summary>
        ///     Contains the ID of the brand.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Contains the code of the brand.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Contains the name of the brand.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Creates a new instance of a <see cref="Brand"/> object.
        /// </summary>
        public Brand()
        {
            Id = 0;
            Code = string.Empty;
            Name = string.Empty;
        }
    }
}
