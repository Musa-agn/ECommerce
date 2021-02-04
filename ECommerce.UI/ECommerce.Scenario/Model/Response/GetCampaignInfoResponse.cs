

namespace ECommerce.Scenario.Model.Response
{
    public class GetCampaignInfoResponse
    {
        public string Status { get; set; }
        public int TargetSales { get; set; }
        public int TotalSales { get; set; }
        public decimal Turnover { get; set; }
        public decimal AverageItemPrice { get; set; }
    }
}
