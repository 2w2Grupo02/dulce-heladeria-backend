using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly IDepositManager _depositManager;
        public DepositController(IDepositManager depositManager)
        {
            _depositManager = depositManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDeposits()
        {
            List<GetDepositDto> result = await _depositManager.GetAllDeposits();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDeposit([FromBody] DepositDto deposit)
        {
            if (deposit == null)
            {
                return BadRequest(ModelState);
            }

            bool result = await _depositManager.InsertDeposit(deposit);

            if (!result)
            {
                ModelState.AddModelError("error prueba", "Error al insertar nuevo deposito");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }

    }
}
