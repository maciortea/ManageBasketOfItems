using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    public class BasketItem : BaseEntity
    {
        private int _productId;
        private int _quantity;
        private decimal _unitPrice;

        public int ProductId
        {
            get => _productId;
            set
            {
                Contract.Require(value > 0, "Product id must be greater than 0");
                _productId = value;
            }
        }
        public int Quantity
        {
            get => _quantity;
            set
            {
                Contract.Require(value > 0, "Quantity must be greater than 0");
                _quantity = value;
            }
        }

        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                Contract.Require(value > 0m, "Price must be greater than 0");
                _unitPrice = value;
            }
        }

        public Product Product { get; set; }

        public void ChangeQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
