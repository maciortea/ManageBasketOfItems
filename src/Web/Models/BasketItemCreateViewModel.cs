using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BasketItemCreateViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPriceInPounds { get; set; }
    }
}
