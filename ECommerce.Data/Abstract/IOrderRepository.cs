using ECommerce.Data.Entity;

namespace ECommerce.Data.Abstract
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
