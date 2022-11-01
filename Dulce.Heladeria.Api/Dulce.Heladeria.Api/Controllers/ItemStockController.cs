using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemStockController : ControllerBase
    {
        private readonly IItemStockManager _itemStockManager;
        public ItemStockController(IItemStockManager itemStockManager)
        {
            _itemStockManager = itemStockManager;
        }

        [HttpPost("movement")]
        public async Task<IActionResult> InsertStockMovement([FromBody] StockMovementDto movement)
        {
            if (movement == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool result = await _itemStockManager.InsertStockMovement(movement);

                if (!result)
                {
                    ModelState.AddModelError("error prueba", "Error al realizar el movimiento");
                    return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableLocations(int itemId, int depositId)
        {
            ItemStockLocationDto result = await _itemStockManager.GetAvailableLocations(itemId, depositId);

            return Ok(result);
        }
    }
}
