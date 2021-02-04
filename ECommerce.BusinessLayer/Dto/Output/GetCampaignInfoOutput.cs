namespace ECommerce.BusinessLayer.Dto.Output
{
    public class GetCampaignInfoOutput
    {
        public string Status { get; set; }
        public int TargetSales { get; set; }
        public int TotalSales { get; set; }
        public decimal Turnover { get; set; }
        public decimal AverageItemPrice { get; set; }
    }
}
