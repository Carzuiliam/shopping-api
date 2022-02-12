namespace shopping_api.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Department()
        {
            Id = 0;
            Name = string.Empty;
        }
    }
}
