using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using CSharpFunctionalExtensions;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBasketService _basketService;

        public BasketController(
            IMapper mapper,
            SignInManager<ApplicationUser> signInManager,
            IBasketService basketService)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            //var user = User.Identity.Name;

            // this should depend on logged user
            int basketId = 1;
            Result<Basket> result = await _basketService.GetBasketById(basketId);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            var basket = _mapper.Map<BasketViewModel>(result.Value);
            return Ok(basket);
        }
    }
}
