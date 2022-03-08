namespace Shopping_API.Models
{
    /// <summary>
    ///     Defines a object which represents a product department.
    /// </summary>
    public class Department
    {
        /// <summary>
        ///     Contains the ID of the department.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Contains the name of the brand.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Creates a new instance of a <see cref="Department"/> object.
        /// </summary>
        public Department()
        {
            Id = 0;
            Name = string.Empty;
        }
    }
}
