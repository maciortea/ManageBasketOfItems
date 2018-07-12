using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers.Api
{
    [Route("api/basket/items")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;

        public BasketItemController(IMapper mapper, IBasketService basketService)
        {
            _mapper = mapper;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketItems()
        {
            // this should depend on logged user
            int basketId = 1;
            Result<IReadOnlyCollection<BasketItem>> result = await _basketService.GetBasketItems(basketId);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            var basketItems = _mapper.Map<List<BasketItemViewModel>>(result.Value);
            return Ok(basketItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(BasketItemViewModel model)
        {
            // this should depend on logged user
            int basketId = 1;
            Result result = await _basketService.AddItemToBasket(basketId, model.ProductId, model.Quantity, model.UnitPrice);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItemFromBasket(int id)
        {
            // this should depend on logged user
            int basketId = 1;
            Result result = await _basketService.RemoveItemFromBasket(basketId, id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> ClearAllItems()
        {
            // this should depend on logged user
            int basketId = 1;
            Result result = await _basketService.ClearAllItems(basketId);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeItemQuantity(int id, [FromBody] int quantity)
        {
            // this should depend on logged user
            int basketId = 1;
            Result result = await _basketService.ChangeItemQuantity(basketId, id, quantity);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }
    }
}