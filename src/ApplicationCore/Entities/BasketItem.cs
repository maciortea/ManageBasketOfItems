using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    public class BasketItem : BaseEntity
    {
        public const int MaxQuantity = 100;

        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public Pounds UnitPriceInPounds { get; private set; }

        private BasketItem()
        {
        }

        public BasketItem(int productId, int quantity, Pounds unitPriceInPounds)
        {
            Contract.Require(productId > 0, ErrorMessage.ProductIdGreaterThanZero);
            Contract.Require(quantity > 0, ErrorMessage.QuantityGreaterThanZero);
            Contract.Require(unitPriceInPounds != null, ErrorMessage.PriceRequired);
            Contract.Require(!unitPriceInPounds.IsZero, ErrorMessage.PriceCannotBeZero);

            ProductId = productId;
            Quantity = quantity;
            UnitPriceInPounds = unitPriceInPounds;
        }

        public void ChangeQuantity(int quantity)
        {
            Contract.Require(quantity > 0, ErrorMessage.QuantityGreaterThanZero);
            Quantity = quantity;
        }
    }
}
