namespace Web.Models
{
    public class BasketItemViewModel
    {
        public int Id { get; set; }
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
        public decimal PriceInPounds { get; set; }
    }
}
