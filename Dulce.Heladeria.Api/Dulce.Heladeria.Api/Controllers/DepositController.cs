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
    public class DepositController : ControllerBase
    {
        private readonly IDepositManager _depositManager;
        public DepositController(IDepositManager depositManager)
        {
            _depositManager = depositManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDeposits()
        {
            List<GetDepositsDto> result = await _depositManager.GetAllDeposits();

            return Ok(result);
        }
    }
}
