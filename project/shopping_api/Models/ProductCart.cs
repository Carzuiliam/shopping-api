namespace shopping_api.Models
{
    public class ProductCart
    {
        public int Id { get; set; }

        public int CartId { get; set; }
        
        public int ProductId { get; set; }

        public decimal Price { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal Total{ get; set; }
        
        public DateTime AddedAt { get; set; }

        public Product? Product { get; set; }

        public ProductCart()
        {
            Id = 0;
            CartId = 0;
            ProductId = 0;
            Price = decimal.Zero;
            Quantity = 0;
            Total = decimal.Zero;
            AddedAt = DateTime.Now;
            Product = null;
        }
    }
}
