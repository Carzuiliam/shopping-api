namespace shopping_api.Models
{
    public class Coupon
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
        
        public decimal Discount { get; set; }

        public Coupon()
        {
            Id = 0;
            Code = string.Empty;
            Description = string.Empty;
            Discount = decimal.Zero;
        }
    }    
}
