using ECommerce.API.Model.Request;
using ECommerce.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest createOrderRequest)
        {
            _orderService.CreateOrder(createOrderRequest.ParseCreateOrderRequest());
            return Ok();
        }
    }
}
