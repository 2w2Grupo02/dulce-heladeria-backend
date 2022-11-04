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
    public class ItemController : ControllerBase
    {
        private readonly IItemManager _itemManager;
        private readonly IItemStockManager _itemStockManager;
        public ItemController(IItemManager itemManager, IItemStockManager itemStockManager)
        {
            _itemManager = itemManager;
            _itemStockManager = itemStockManager;
        }

        [HttpPost]
        public async Task<IActionResult> InsertItem([FromBody] ItemDto item)
        {
            if(item == null)
            {
                return BadRequest(ModelState);
            }

            var result = await _itemManager.InsertItem(item);

            if (!result)
            {
                ModelState.AddModelError("error prueba", "Error al insertar nuevo intem");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();

        }
        [HttpGet("{itemId}/stock")]
        public async Task<IActionResult> GetItemStock([FromRoute] int itemId)
        {
            if (itemId == 0)
            {
                return BadRequest(ModelState);
            }

            List<ItemStockDto> result = await _itemStockManager.GetItemStock(itemId);


            return Ok(result);

        }
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            List<GetItemsDto> result = await _itemManager.GetAllItems();


            return Ok(result);

        }
    }
}
