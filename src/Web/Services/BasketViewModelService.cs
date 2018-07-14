using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;

        public BasketViewModelService(IMapper mapper, IBasketService basketService)
        {
            _mapper = mapper;
            _basketService = basketService;
        }

        public async Task<BasketViewModel> GetOrCreateBasketForUserAsync(string userName)
        {
            Basket basket = await _basketService.GetBasketByUserId(userName);
            if (basket == null)
            {
                return await CreateBasketForUserAsync(userName);
            }
            return _mapper.Map<BasketViewModel>(basket);
        }

        private async Task<BasketViewModel> CreateBasketForUserAsync(string userId)
        {
            var basket = new Basket { UserId = userId };
            await _basketService.AddBasket(basket);

            return new BasketViewModel
            {
                Id = basket.Id,
                UserId = basket.UserId,
                Items = new List<BasketItemViewModel>()
            };
        }
    }
}
