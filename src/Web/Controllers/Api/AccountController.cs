using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers.Api
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IAppLogger<AccountController> _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config,
            IAppLogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model.");
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var createResult = await _userManager.CreateAsync(user, model.Password);
            if (!createResult.Succeeded)
            {
                return BadRequest("Failed to register.");
            }
            return Ok("User registered succesfully.");
        }

        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model.");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("User does not exist.");
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!signInResult.Succeeded)
            {
                return BadRequest("Invalid e-mail or password.");
            }

            var tokenKey = _config["Tokens:Key"];
            var issuer = _config["Tokens:Issuer"];
            var audience = _config["Tokens:Audience"];

            var token = JwtTokenHelper.GenerateToken(user, tokenKey, issuer, audience);

            var result = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            return Created(string.Empty, result);
        }
    }
}
