using System;

namespace ECommerce.BusinessLayer.Dto.Output
{
    public class GetCampaignInfoByProductCodeOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int TargetSales { get; set; }
        public int TotalSales { get; set; }
    }
}
