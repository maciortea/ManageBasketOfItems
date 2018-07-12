using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers.Api
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;

        public BasketController(IMapper mapper, IBasketService basketService)
        {
            _mapper = mapper;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            // this should depend on logged user
            int basketId = 1;
            Result<Basket> result = await _basketService.GetBasket(basketId);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            var basket = _mapper.Map<BasketViewModel>(result.Value);
            return Ok(basket);
        }
    }
}
