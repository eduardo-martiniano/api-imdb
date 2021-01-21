using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api_imdb.Configuration;
using api_imdb.Contracts.IRepositories;
using api_imdb.Models.Jsons;
using api_imdb.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace api_imdb.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Registrar-se como usuario ou administrador (0 - Usuario/ 1 - Adm)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Token de acesso</returns>
        /// <response code="201">Token</response>
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

                await _userRepository.CreateClaims(new IdentityUserClaim<string>
                {
                    UserId = user.Id,
                    ClaimType = model.TypeOfUser.ToString(),
                    ClaimValue = ""
                });

                await _userRepository.AddToTableUser(user.Id);

                return StatusCode(201, await GenerateJwt(model.Email));
            }

            return StatusCode(400, "Você já possui cadastro!");
        }

        /// <summary>
        /// Fazer login
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Token de acesso</returns>
        /// <response code="202">Token</response>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Dados invalidos");

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (!await _userRepository.UserActived(model.Email)) return NotFound("Essa conta está desativada");

            if (result.Succeeded) return StatusCode(202, await GenerateJwt(model.Email));

            if (result.IsLockedOut) return BadRequest("Usuario travado");

            return BadRequest("usuario ou senha invalidos");
        }

        /// <summary>
        /// Desativar conta
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Token de acesso</returns>
        /// <response code="204">Token</response>
        [HttpDelete]
        [Route("desactive")]
        public async Task<ActionResult> DesactiveAccount([FromBody] LoginUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Dados invalidos");

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (!await _userRepository.UserActived(model.Email)) return NotFound("Essa conta está desativada");

            if (result.Succeeded)
            {
                await _userRepository.DesactiveOrActiveAccount(model.Email);
                return StatusCode(204, "Conta desativada com sucesso!");
            }

            return BadRequest("usuario ou senha invalidos");
        }

        /// <summary>
        /// Ativar conta
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Token de acesso</returns>
        /// <response code="204">Token</response>
        [HttpPut]
        [Route("active")]
        public async Task<ActionResult> ActiveAccount([FromBody] LoginUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Dados invalidos");

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (await _userRepository.UserActived(model.Email)) return NotFound("Essa conta já está ativada!");

            if (result.Succeeded)
            {
                await _userRepository.DesactiveOrActiveAccount(model.Email);
                return StatusCode(204, "Conta ativada com sucesso!");
            }

            return BadRequest("usuario ou senha invalidos");
        }

        /// <summary>
        /// Alterar senha
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Token de acesso</returns>
        /// <response code="204">Token</response>
        [HttpPut]
        [Route("update-password")]
        public async Task<ActionResult> Update([FromBody] UpdateUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Dados invalidos");

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (!await _userRepository.UserActived(model.Email)) return NotFound("Essa conta está desativada");

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                return NoContent();
            }

            return BadRequest("usuario ou senha invalidos");
        }

        /// <summary>
        /// Obter usuarios
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Token de acesso</returns>
        /// <response code="200">Token</response>
        [HttpGet]
        [Route("users")]
        [ClaimsAuthorize("ADM", "Add")]
        public async Task<IActionResult> GetUsers([FromQuery] int limit = 30, [FromQuery] int offset = 0)
        {
            var users = await _userRepository.GetUsers(limit, offset);
            var userJson = users.Select(a => new UserJson(a)).ToList();
            return Ok(userJson);
        }

        private async Task<TokenJson> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emitter,
                Audience = _appSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);
            return new TokenJson(encodedToken);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }

}