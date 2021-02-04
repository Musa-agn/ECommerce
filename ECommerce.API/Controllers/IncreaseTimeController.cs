using ECommerce.API.Model.Request;
using ECommerce.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncreaseTimeController : ControllerBase
    {
        private readonly IIncreaseTimeService _increaseTimeService;
        public IncreaseTimeController(IIncreaseTimeService increaseTimeService)
        {
            _increaseTimeService = increaseTimeService;
        }
        [HttpPut]
        public IActionResult IncreaseTime([FromBody] IncreaseTimeRequest increaseTimeRequest)
        {
            return Ok(_increaseTimeService.IncreaseTime(increaseTimeRequest.Hour));
        }
    }
}
