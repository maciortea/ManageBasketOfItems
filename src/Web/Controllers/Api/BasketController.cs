using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

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
        private readonly IBasketViewModelService _basketViewModelService;

        public BasketController(
            IMapper mapper,
            SignInManager<ApplicationUser> signInManager,
            IBasketService basketService,
            IBasketViewModelService basketViewModelService)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _basketService = basketService;
            _basketViewModelService = basketViewModelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            BasketViewModel basket = await _basketViewModelService.GetOrCreateBasketForUserAsync(User.Identity.Name);
            return Ok(basket);
        }
    }
}
