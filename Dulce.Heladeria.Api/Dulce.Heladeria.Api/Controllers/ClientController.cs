using Dulce.Heladeria.Models.Enums;
using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.Helper;
using Dulce.Heladeria.Services.IManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Api.Controllers
{
    [Authorize]
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
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            List<GetClientsDto> result = await _clientManager.GetAllClients();


            return Ok(result);

        }
        [HttpGet("name")]
        public async Task<IActionResult> GetClientByName([FromQuery]GetClientDto client)
        {
            try
            {
                GetClientsDto result = await _clientManager.GetClientByName(client);

                if (result == null)
                {
                    return NotFound("El cliente no existe");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientDto client)
        {
           // var role = AuthHelper.GetRole(Request);
            //if (role != Roles.administrator.ToString())
           // {
            //    return Unauthorized();
            //}

            try
            {
                bool result = await _clientManager.UpdateClient(id, client);

                if (!result)
                {
                    return BadRequest("Error al actualizar el cliente");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
