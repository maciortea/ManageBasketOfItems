using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    public class BasketItem : BaseEntity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public void ChangeQuantity(int quantity)
        {
            Contract.Require(quantity > 0, "At least one item is required");

            Quantity = quantity;
        }
    }
}
