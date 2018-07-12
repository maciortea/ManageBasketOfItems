﻿using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
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

        public async Task<Result<Basket>> GetBasket(int basketId)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                var message = string.Format(ErrorMessage.BasketDoesntExists, basketId);
                _logger.LogInformation(message);
                return Result.Fail<Basket>(message);
            }

            return Result.Ok(basket);
        }

        public async Task<Result<IReadOnlyCollection<BasketItem>>> GetBasketItems(int basketId)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                var message = string.Format(ErrorMessage.BasketDoesntExists, basketId);
                _logger.LogInformation(message);
                return Result.Fail<IReadOnlyCollection<BasketItem>>(message);
            }

            return Result.Ok(basket.Items);
        }

        public async Task<Result> AddItemToBasket(int basketId, int productId, int quantity, decimal unitPrice)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                var message = string.Format(ErrorMessage.BasketDoesntExists, basketId);
                _logger.LogInformation(message);
                return Result.Fail(message);
            }

            basket.AddItem(productId, quantity, unitPrice);
            await _basketRepository.UpdateAsync(basket);
            return Result.Ok();
        }

        public async Task<Result> RemoveItemFromBasket(int basketId, int basketItemId)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                var message = string.Format(ErrorMessage.BasketDoesntExists, basketId);
                _logger.LogInformation(message);
                return Result.Fail(message);
            }

            Result result = basket.RemoveItem(basketItemId);
            await _basketRepository.UpdateAsync(basket);
            return result;
        }

        public async Task<Result> ClearAllItems(int basketId)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                var message = string.Format(ErrorMessage.BasketDoesntExists, basketId);
                _logger.LogInformation(message);
                return Result.Fail(message);
            }

            basket.Clear();
            await _basketRepository.UpdateAsync(basket);
            return Result.Ok();
        }

        public async Task<Result> ChangeItemQuantity(int basketId, int basketItemId, int quantity)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket == null)
            {
                var message = string.Format(ErrorMessage.BasketDoesntExists, basketId);
                _logger.LogInformation(message);
                return Result.Fail(message);
            }

            BasketItem item = basket.Items.FirstOrDefault(i => i.Id == basketItemId);
            if (item == null)
            {
                var message = string.Format(ErrorMessage.BasketItemDoesntExists, basketItemId);
                _logger.LogInformation(message);
                return Result.Fail(message);
            }

            item.ChangeQuantity(quantity);
            await _basketRepository.UpdateAsync(basket);
            return Result.Ok();
        }
    }
}