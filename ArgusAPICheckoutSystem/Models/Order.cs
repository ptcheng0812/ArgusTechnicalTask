namespace ArgusAPICheckoutSystem.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int People { get; set; }
        public int Starters { get; set; }
        public int Mains { get; set; }
        public int Drinks { get; set; }  // Full-price drinks
        public int DrinksWithDiscount { get; set; }  // Discounted drinks
    }
}
