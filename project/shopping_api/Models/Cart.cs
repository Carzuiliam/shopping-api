namespace shopping_api.Models
{
    public class Cart
    {
        public int Id { get; set; }
                
        public decimal Subtotal { get; set; }
        
        public decimal Discount{ get; set; }
        
        public decimal Shipping { get; set; }
        
        public decimal Total { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }

        public Coupon? Coupon { get; set; }

        public List<ProductCart> ProductCarts { get; set; }

        public Cart()
        {
            Id = 0;
            Subtotal = decimal.Zero;
            Discount = decimal.Zero;
            Shipping = decimal.Zero;
            Total = decimal.Zero;
            CreatedAt = DateTime.Now;
            User = new();
            Coupon = null;
            ProductCarts = new();
        }
    }
}
