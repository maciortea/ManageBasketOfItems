using ApplicationCore.Common;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entities
{
    public class Basket : BaseEntity
    {
        private readonly List<BasketItem> _items;
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

        public string UserId { get; set; }

        public Basket()
        {
            _items = new List<BasketItem>();
        }

        public virtual BasketItem AddItem(int productId, int quantity, Pounds priceInPounds)
        {
            BasketItem basketItem;
            bool itemAlreadyExists = _items.Any(i => i.ProductId == productId);
            if (!itemAlreadyExists)
            {
                basketItem = new BasketItem(productId, quantity, priceInPounds);
                _items.Add(basketItem);
            }
            else
            {
                basketItem = _items.First(i => i.ProductId == productId);
                int newQuantity = basketItem.Quantity + quantity;
                basketItem.ChangeQuantity(newQuantity);
            }
            return basketItem;
        }

        public virtual Result RemoveItem(int itemId)
        {
            BasketItem item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                var message = string.Format(ErrorMessage.BasketWithItemIdDoesntExists, itemId);
                return Result.Fail(message);
            }

            _items.Remove(item);
            return Result.Ok();
        }

        public virtual void Clear()
        {
            _items.Clear();
        }
    }
}
