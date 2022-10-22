using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemStockController : ControllerBase
    {
        private readonly IItemStockManager _itemStockManager;
        public ItemStockController(IItemStockManager itemManager)
        {
            _itemStockManager = itemManager;
        }

        [HttpPost]
        public async Task<IActionResult> GetStockItem([FromBody] int itemId)
        {
            if (itemId == 0)
            {
                return BadRequest(ModelState);
            }

            List<ItemStockDto> result = await _itemStockManager.GetStockItem(itemId);

            //if (result == null)
            //{
            //    ModelState.AddModelError("error prueba", "Error al insertar nuevo intem");
            //    return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            //}

            return Ok(result);

        }
    }
}
