namespace shopping_api.Models
{
    public class Brand
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Brand()
        {
            Id = 0;
            Code = string.Empty;
            Name = string.Empty;
        }
    }
}
