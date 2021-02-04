using System;

namespace ECommerce.BusinessLayer.Dto.Input
{
    public class UpdateCampaignInfoInput
    {
        public Guid CampaignId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductCode { get; set; }
    }
}
