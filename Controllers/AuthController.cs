using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_imdb.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api_imdb.Controllers
{
    [Route("account")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;


        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Dados invalidos");

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok(model);
            }

            return Ok("Algo deu errado");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Dados invalidos");

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (result.Succeeded)
            {
                return Ok();
            }

            if (result.IsLockedOut)
            {
                return BadRequest("Usuario travado");
            }

            return Ok("usuario e senha invalidos");
        }
    }
}