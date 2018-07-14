using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BasketItemCreateViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Required]
        [Range(1, BasketItem.MaxQuantity)]
        public int Quantity { get; set; }

        [Required]
        [Range(1, (double)Pounds.MaxPoundAmount)]
        public decimal UnitPriceInPounds { get; set; }
    }
}
