using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;

namespace ECommerce.Data.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        public void CreateOrder(Order order)
        {
            OrderList.Orders.Add(order);
        }
    }
}
