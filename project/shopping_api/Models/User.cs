namespace shopping_api.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public User()
        {
            Id = 0;
            Username = string.Empty;
            Name = string.Empty;
        }
    }    
}
