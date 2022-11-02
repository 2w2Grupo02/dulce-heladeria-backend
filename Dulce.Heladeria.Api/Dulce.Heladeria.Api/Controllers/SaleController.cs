using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleManager _saleManager;
        public SaleController(ISaleManager saleManager)
        {
            _saleManager = saleManager;
        }
        [HttpPost]
        public async Task<IActionResult> InsertNewSale([FromBody] SaleDto saleDto)
        {
            if (saleDto == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _saleManager.InsertNewSale(saleDto);

                if (!result)
                {
                    ModelState.AddModelError("error prueba", "Error al insertar nueva venta");
                    return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
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
