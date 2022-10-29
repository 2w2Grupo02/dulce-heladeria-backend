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
    public class LocationController : ControllerBase
    {
        private readonly ILocationManager _locationManager;
        public LocationController(ILocationManager locationManager)
        {
            _locationManager = locationManager;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllLocations()
        //{
        //    List<GetLocationDto> result = await _depositManager.GetAllDeposits();

        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> InsertLocation([FromBody] LocationDto location)
        {
            if (location == null)
            {
                return BadRequest(ModelState);
            }

            bool result = await _locationManager.InsertLocation(location);

            if (!result)
            {
                ModelState.AddModelError("error prueba", "Error al insertar nueva ubicacion");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }
    }
}
