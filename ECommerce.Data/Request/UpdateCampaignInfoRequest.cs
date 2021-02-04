using System;

namespace ECommerce.Data.Request
{
    public class UpdateCampaignInfoRequest
    {
        public UpdateCampaignInfoRequest(Guid campaignId, int totalSales, decimal turnover, decimal averageItemPrice, int status)
        {
            CampaignId = campaignId;
            TotalSales = totalSales;
            Turnover = turnover;
            AverageItemPrice = averageItemPrice;
            Status = status;
        }
        public Guid CampaignId { get; set; }
        public int TotalSales { get; set; }
        public decimal Turnover { get; set; }
        public decimal AverageItemPrice { get; set; }
        public int Status { get; set; }
    }
}
