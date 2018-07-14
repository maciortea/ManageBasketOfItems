using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    public class BasketItem : BaseEntity
    {
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public Pounds UnitPriceInPounds { get; private set; }

        private BasketItem()
        {
        }

        public BasketItem(int productId, int quantity, Pounds unitPriceInPounds)
        {
            Contract.Require(productId > 0, "Product id must be greater than 0");
            Contract.Require(quantity > 0, "Quantity must be greater than 0");
            Contract.Require(unitPriceInPounds != null, "Price is required");
            Contract.Require(!unitPriceInPounds.IsZero, "Price cannot be 0");

            ProductId = productId;
            Quantity = quantity;
            UnitPriceInPounds = unitPriceInPounds;
        }

        public void ChangeQuantity(int quantity)
        {
            Contract.Require(quantity > 0, "Quantity must be greater than 0");
            Quantity = quantity;
        }
    }
}
