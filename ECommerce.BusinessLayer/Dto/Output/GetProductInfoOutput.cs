using System;

namespace ECommerce.BusinessLayer.Dto.Output
{
    public class GetProductInfoOutput
    {
        public Guid? CampaignId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
