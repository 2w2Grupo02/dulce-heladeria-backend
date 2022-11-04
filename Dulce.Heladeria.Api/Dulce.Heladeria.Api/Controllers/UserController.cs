using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Dulce.Heladeria.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IConfiguration _config;
        public UserController(IUserManager userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto user)
        {
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _userManager.Register(user);

                if (!result)
                {
                    ModelState.AddModelError("error", "Error al insertar nuevo usuario");
                    return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(UserLoginDto usuarioAuthLoginDto)
        {
            var userDto = await _userManager.Login(usuarioAuthLoginDto.User, usuarioAuthLoginDto.Password);
            if (userDto is null)
            {
                return Unauthorized("Contraseña o usuario incorrecto");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userDto.Id.ToString()),
                new Claim(ClaimTypes.Name,userDto.Email.ToString()),
                new Claim(ClaimTypes.Role, userDto.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                userId = userDto.Id,
                role = userDto.Role,
                accessToken = tokenHandler.WriteToken(token)
            });

        }
    }
}
