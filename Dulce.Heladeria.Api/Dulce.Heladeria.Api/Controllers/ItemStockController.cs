using Dulce.Heladeria.Services.Dtos;
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

        [HttpPost()]
        public async Task<IActionResult> NewEntryToStock([FromBody] NewItemStockDto newEntry)
        {
            if (newEntry == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool result = await _itemStockManager.NewEntryToStock(newEntry);

                if (!result)
                {
                    ModelState.AddModelError("error prueba", "Error al insertar articulos al stock");
                    return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpGet("locations")]
        public async Task<IActionResult> GetLocations(int itemId, int depositId)
        {
            try
            {
                List<DestinationLocationDto> result = await _itemStockManager.GetLocations(itemId, depositId);

                if (result == null)
                {
                    ModelState.AddModelError("error", "Error al obtener ubicaciones");
                    return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
