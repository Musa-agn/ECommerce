using ECommerce.API.Model.Request;
using ECommerce.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductRequest createProductRequest)
        {
            _productService.CreateProduct(createProductRequest.ParseCreateProductRequest());
            return Ok();
        }

        [HttpGet]
        public IActionResult GetProductInfo([FromQuery] string productCode)
        {
            return Ok(_productService.GetProductInfo(productCode));
        }
    }
}
