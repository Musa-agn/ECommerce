using System;

namespace ECommerce.Data.Entity
{
    public class Order
    {
        public Order(string productCode, int quantity, decimal price, Guid? campaignId = null)
        {
            Id = Guid.NewGuid();
            ProductCode = productCode;
            Quantity = quantity;
            Price = price;
            CampaignId = campaignId ?? Guid.Empty;
            Status = (int)OrderStatusEnum.Sale;
        }
        public Guid Id { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid CampaignId { get; set; }
        public int Status { get; set; }
    }
}
