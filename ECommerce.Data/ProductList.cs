using ECommerce.Data.Entity;
using System.Collections.Generic;

namespace ECommerce.Data
{
    public static class ProductList
    {
        public static List<Product> Products;
        static ProductList()
        {
            Products = new List<Product>();
        }
    }
}
