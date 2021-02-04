using ECommerce.BusinessLayer.Dto.Input;

namespace ECommerce.BusinessLayer.Abstract
{
    public interface IOrderService
    {
        void CreateOrder(CreateOrderInput createOrderInput);

    }
}
