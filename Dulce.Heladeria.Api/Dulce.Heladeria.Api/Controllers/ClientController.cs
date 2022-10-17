using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientManager _clientManager;
        public ClientController(IClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        [HttpPost]
        public async Task<IActionResult> InsertClient([FromBody] ClientDto client)
        {
            if (client == null)
            {
                return BadRequest(ModelState);
            }

            var result = await _clientManager.InsertClient(client);

            if (!result)
            {
                ModelState.AddModelError("error", "Error al insertar nuevo cliente");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();

        }
    }
}