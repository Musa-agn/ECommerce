using System;

namespace ECommerce.Data.Entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public Product(string code, decimal price, int stock)
        {
            Id = Guid.NewGuid();
            Code = code;
            Price = price;
            Stock = stock;
            Discount = 0;
        }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public decimal Discount { get; set; }
    }
}
