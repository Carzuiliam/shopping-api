namespace Shopping_API.Models
{
    /// <summary>
    ///     Defines a object which represents a user.
    /// </summary>
    public class User
    {
        /// <summary>
        ///     Contains the ID of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Contains the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Contains the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Creates a new instance of an <see cref="User"/> object.
        /// </summary>
        public User()
        {
            Id = 0;
            Username = string.Empty;
            Name = string.Empty;
        }
    }    
}
