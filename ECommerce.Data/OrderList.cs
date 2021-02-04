using ECommerce.Data.Entity;
using System.Collections.Generic;

namespace ECommerce.Data
{
    public static class OrderList
    {
        public static List<Order> Orders;
        static OrderList()
        {
            Orders = new List<Order>();
        }
    }
}
