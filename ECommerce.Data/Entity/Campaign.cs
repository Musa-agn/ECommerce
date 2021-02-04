using System;

namespace ECommerce.Data.Entity
{
    public class Campaign
    {
        public Campaign(string name, string productCode, int duration, int priceManipulationLimit, int targetSalesCount)
        {
            Id = Guid.NewGuid();
            Name = name;
            ProductCode = productCode;
            Duration = duration;
            PriceManipulationLimit = priceManipulationLimit;
            TargetSalesCount = targetSalesCount;
            TotalSales = 0;
            Turnover = 0;
            AverageItemPrice = 0;
            Status = (int)CampaignStatusEnum.Active;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
        public int TotalSales { get; set; }
        public decimal Turnover { get; set; }
        public decimal AverageItemPrice { get; set; }
        public int Status { get; set; }
    }
}
