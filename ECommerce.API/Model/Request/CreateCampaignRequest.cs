using ECommerce.BusinessLayer.Dto.Input;

namespace ECommerce.API.Model.Request
{
    public class CreateCampaignRequest
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }

        public CreateCampaignInput ParseCreateCampaignRequest()
        {
            return new CreateCampaignInput
            {
                Name = Name,
                ProductCode = ProductCode,
                Duration = Duration,
                PriceManipulationLimit = PriceManipulationLimit,
                TargetSalesCount = TargetSalesCount
            };
        }
    }
}
