using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CSharpFunctionalExtensions;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IAppLogger<BasketService> _logger;
        private readonly IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository, IAppLogger<BasketService> logger)
        {
            _logger = logger;
            _basketRepository = basketRepository;
        }

        public async Task AddBasketAsync(Basket basket)
        {
            await _basketRepository.AddAsync(basket);
        }

        public async Task<Basket> GetBasketByUserIdAsync(string userId)
        {
            return await _basketRepository.GetByUserIdAsync(userId);
        }

        public async Task<Result> AddItemToBasketAsync(int basketId, int productId, int quantity, Pounds unitPriceInPounds)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                var message = string.Format(ErrorMessage.BasketWithIdDoesntExists, basketId);
                _logger.LogInformation(message);
                return Result.Fail(message);
            }

            basket.AddItem(productId, quantity, unitPriceInPounds);
            await _basketRepository.UpdateAsync(basket);
            return Result.Ok();
        }

        public async Task<Result> RemoveItemFromBasketAsync(string userId, int basketItemId)
        {
            Basket basket = await _basketRepository.GetByUserIdAsync(userId);
            if (basket == null)
            {
                var message = string.Format(ErrorMessage.UserDoesntHaveBasket, userId);
                _logger.LogInformation(message);
                return Result.Fail(message);
            }

            Result result = basket.RemoveItem(basketItemId);
            await _basketRepository.UpdateAsync(basket);
            return result;
        }

        public async Task<Result> ClearAllItemsAsync(string userId)
        {
            Basket basket = await _basketRepository.GetByUserIdAsync(userId);
            if (basket == null)
            {
                var message = string.Format(ErrorMessage.UserDoesntHaveBasket, userId);
                _logger.LogInformation(message);
                return Result.Fail(message);
            }

            basket.Clear();
            await _basketRepository.UpdateAsync(basket);
            return Result.Ok();
        }

        public async Task<Result> ChangeItemQuantityAsync(string userId, int basketItemId, int quantity)
        {
            Basket basket = await _basketRepository.GetByUserIdAsync(userId);
            if (basket == null)
            {
                var message = string.Format(ErrorMessage.UserDoesntHaveBasket, userId);
                _logger.LogInformation(message);
                return Result.Fail(message);
            }

            BasketItem item = basket.Items.FirstOrDefault(i => i.Id == basketItemId);
            if (item == null)
            {
                var message = string.Format(ErrorMessage.BasketWithItemIdDoesntExists, basketItemId);
                _logger.LogInformation(message);
                return Result.Fail(message);
            }

            item.ChangeQuantity(quantity);
            await _basketRepository.UpdateAsync(basket);
            return Result.Ok();
        }
    }
}
