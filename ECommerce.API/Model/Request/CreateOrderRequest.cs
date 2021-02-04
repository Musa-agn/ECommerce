using ECommerce.BusinessLayer.Dto.Input;

namespace ECommerce.API.Model.Request
{
    public class CreateOrderRequest
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }

        public CreateOrderInput ParseCreateOrderRequest()
        {
            return new CreateOrderInput
            {
                ProductCode = ProductCode,
                Quantity = Quantity
            };
        }
    }
}
