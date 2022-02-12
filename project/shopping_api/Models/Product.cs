namespace shopping_api.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public Brand? Brand { get; set; }
        
        public Department? Department { get; set; }

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
