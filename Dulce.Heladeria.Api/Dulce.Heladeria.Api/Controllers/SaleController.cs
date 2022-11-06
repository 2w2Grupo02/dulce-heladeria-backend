using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using Microsoft.AspNetCore.Authorization;
using Dulce.Heladeria.Services.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dulce.Heladeria.Models.Enums;
using Dulce.Heladeria.Services.Helper;

namespace Dulce.Heladeria.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleManager _saleManager;
        public SaleController(ISaleManager saleManager)
        {
            _saleManager = saleManager;
        }

        [HttpPost("/range")]
        public async Task<IActionResult> GetAllSalesAmountPerDay([FromBody] RangeDto rangeDto)
        {
            List<SalePerDayDto> result = await _saleManager
                .getAllSales(rangeDto.start, rangeDto.end);
            return Ok(result);
        }


        [HttpGet("/range")]
        public async Task<IActionResult> GetAllSalesAmountPerDay2([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            List<SalePerDayDto> result = await _saleManager
                .getAllSales(start, end);
            return Ok(result);
        }

        [HttpGet("/day")]
        public async Task<IActionResult> GetAllWithPaymentMethod([FromQuery] DateTime start)
        {
            var result = await _saleManager.getAllSalesByMethod(start); 
            return Ok(result);
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

        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            List<GetSaleDto> result = await _saleManager.GetSales();

            return Ok(result);
        }        
    }
}
