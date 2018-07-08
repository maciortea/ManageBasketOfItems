using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entities
{
    public class Basket : BaseEntity
    {
        //public string BuyerId { get; set; }
        private readonly List<BasketItem> _items = new List<BasketItem>();
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

        public Basket()
        {
        }

        public void AddItem(int productId, int quantity, decimal unitPrice)
        {
            bool itemAlreadyExists = _items.Any(i => i.ProductId == productId);
            if (!itemAlreadyExists)
            {
                _items.Add(new BasketItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                });
                return;
            }

            BasketItem existingItem = _items.First(i => i.ProductId == productId);
            int newQuantity = existingItem.Quantity + quantity;
            existingItem.ChangeQuantity(newQuantity);
        }

        public void RemoveItem(int itemId)
        {
            BasketItem item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                // throw exception
            }

            _items.Remove(item);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
