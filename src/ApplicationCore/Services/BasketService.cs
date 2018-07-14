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

        public async Task AddBasket(Basket basket)
        {
            await _basketRepository.AddAsync(basket);
        }

        public async Task<Basket> GetBasketByUserId(string userId)
        {
            return await _basketRepository.GetByUserId(userId);
        }

        public async Task<Result> AddItemToBasket(int basketId, int productId, int quantity, decimal unitPrice)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                var message = string.Format(ErrorMessage.BasketWithIdDoesntExists, basketId);
                _logger.LogInformation(message);
                return Result.Fail(message);
            }

            basket.AddItem(productId, quantity, unitPrice);
            await _basketRepository.UpdateAsync(basket);
            return Result.Ok();
        }

        public async Task<Result> RemoveItemFromBasket(string userId, int basketItemId)
        {
            Basket basket = await _basketRepository.GetByUserId(userId);
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

        public async Task<Result> ClearAllItems(string userId)
        {
            Basket basket = await _basketRepository.GetByUserId(userId);
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

        public async Task<Result> ChangeItemQuantity(string userId, int basketItemId, int quantity)
        {
            Basket basket = await _basketRepository.GetByUserId(userId);
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
