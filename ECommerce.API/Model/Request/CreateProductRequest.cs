using ECommerce.BusinessLayer.Dto.Input;

namespace ECommerce.API.Model.Request
{
    public class CreateProductRequest
    {
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public CreateProductInput ParseCreateProductRequest()
        {
            return new CreateProductInput
            {
                Code = Code,
                Price = Price,
                Stock = Stock
            };
        }
    }
}
