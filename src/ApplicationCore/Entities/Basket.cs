using ApplicationCore.Common;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entities
{
    public class Basket : BaseEntity
    {
        private readonly List<BasketItem> _items = new List<BasketItem>();
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

        public string UserId { get; set; }

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
            }
            else
            {
                BasketItem existingItem = _items.First(i => i.ProductId == productId);
                int newQuantity = existingItem.Quantity + quantity;
                existingItem.ChangeQuantity(newQuantity);
            }
        }

        public Result RemoveItem(int itemId)
        {
            BasketItem item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                var message = string.Format(ErrorMessage.BasketItemDoesntExists, itemId);
                return Result.Fail(message);
            }

            _items.Remove(item);
            return Result.Ok();
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
