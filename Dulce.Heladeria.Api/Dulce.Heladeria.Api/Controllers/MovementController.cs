using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Models.Enums;
using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.Helper;
using Dulce.Heladeria.Services.IManager;
using Dulce.Heladeria.Services.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly IStockMovementManager _movementManager; 
        public MovementController(IStockMovementManager movementManager)
        {
            _movementManager = movementManager; 
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllMovements()
        {
            List<MovementDto> result = await _movementManager.GetAll();

            return Ok(result);
        }
        
        [HttpGet("dates")]
        public IActionResult GetByDate([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            List<MovementDto> result = _movementManager.GetAllByDate(start,end);

            return Ok(result);
        }

        [HttpGet("item")]
        public IActionResult GetByItem([FromQuery] int itemId)
        {
            List<MovementDto> result = _movementManager.GetAllByItemId(itemId); 

            return Ok(result);
        }
    }
}
