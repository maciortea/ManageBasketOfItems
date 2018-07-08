using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task AddItemToBasket(int basketId, int productId, int quantity, decimal unitPrice)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                // throw exception
            }

            basket.AddItem(productId, quantity, unitPrice);
            await _basketRepository.UpdateAsync(basket);
        }

        public async Task RemoveItemFromBasket(int basketId, int basketItemId)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                // throw exception
            }

            basket.RemoveItem(basketItemId);
            await _basketRepository.UpdateAsync(basket);
        }

        public async Task ClearAllItems(int basketId)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                // throw exception
            }

            basket.Clear();
            await _basketRepository.UpdateAsync(basket);
        }

        public async Task ChangeItemQuantity(int basketId, int basketItemId, int quantity)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                // throw exception
            }

            BasketItem item = basket.Items.FirstOrDefault(i => i.Id == basketItemId);
            if (item == null)
            {
                // throw exception
            }

            item.ChangeQuantity(quantity);
            await _basketRepository.UpdateAsync(basket);
        }
    }
}
