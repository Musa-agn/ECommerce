using ECommerce.API.Model.Request;
using ECommerce.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpPost]
        public IActionResult CreateCampaign([FromBody] CreateCampaignRequest createCampaignRequest)
        {
            _campaignService.CreateCampaign(createCampaignRequest.ParseCreateCampaignRequest());
            return Ok();
        }

        [HttpGet]
        public IActionResult GetCampaignInfo([FromQuery] string name)
        {
            return Ok(_campaignService.GetCampaignInfo(name));
        }
    }
}
