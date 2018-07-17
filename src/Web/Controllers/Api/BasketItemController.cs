using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/basket/items")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;
        private readonly IBasketViewModelService _basketViewModelService;

        public BasketItemController(
            IMapper mapper,
            IBasketService basketService,
            IBasketViewModelService basketViewModelService)
        {
            _mapper = mapper;
            _basketService = basketService;
            _basketViewModelService = basketViewModelService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BasketItemViewModel>> GetItem(int id)
        {
            BasketItemViewModel item = await _basketViewModelService.GetBasketItemForUserAsync(User.Identity.Name, id);
            if (item == null)
            {
                return NotFound(string.Format(ErrorMessage.BasketWithItemIdDoesntExists, id));
            }
            return item;
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(BasketItemCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }

            BasketViewModel basket = await _basketViewModelService.GetOrCreateBasketForUserAsync(User.Identity.Name);

            var priceResult = Pounds.Create(model.PriceInPounds);
            if (priceResult.IsFailure)
            {
                return BadRequest(priceResult.Error);
            }

            var result = await _basketService.AddItemToBasketAsync(basket.Id, model.ProductId, model.Quantity, priceResult.Value);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            BasketItemViewModel item = await _basketViewModelService.GetBasketItemForUserAsync(User.Identity.Name, result.Value);
            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItemFromBasket(int id)
        {
            Result result = await _basketService.RemoveItemFromBasketAsync(User.Identity.Name, id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> ClearAllItems()
        {
            Result result = await _basketService.ClearAllItemsAsync(User.Identity.Name);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeItemQuantity(int id, [FromBody] int quantity)
        {
            Result result = await _basketService.ChangeItemQuantityAsync(User.Identity.Name, id, quantity);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return NoContent();
        }
    }
}
