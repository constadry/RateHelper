using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RateHelper.Services;

namespace RateHelper.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLastRate()
        {
            var result = await _rateService.GetLastRate();

            if (!result.Success) return BadRequest(result.Message);

            return Ok(result.Rate);
        }
        
        [HttpGet("{updateTime:datetime}")]
        public async Task<IActionResult> GetLastActiveRate(DateTime updateTime)
        {
            var result = await _rateService.GetLastRate(updateTime);

            if (!result.Success) return BadRequest(result.Message);

            return Ok(result.Rate);
        }
    }
}