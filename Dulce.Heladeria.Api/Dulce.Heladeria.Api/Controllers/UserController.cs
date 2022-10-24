using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] CreateUserDto user)
        {   
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.InsertUser(user);

            if (!result)
            {
                ModelState.AddModelError("error", "Error al insertar nuevo usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return Ok(result);
        }
    }
}
