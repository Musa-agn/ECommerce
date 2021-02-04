using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Dto;
using ECommerce.BusinessLayer.SystemTime;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;

namespace ECommerce.BusinessLayer.Concrete
{
    public class CampaignAlgorithmService : ICampaignAlgorithmService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        enum Percent { tenPercent = 10, thirtyPercent = 30, seventyPercent = 70 };

        public CampaignAlgorithmService(ICampaignRepository campaignRepository, IProductRepository productRepository,
             IProductService productService)
        {
            _campaignRepository = campaignRepository;
            _productRepository = productRepository;
            _productService = productService;
        }
        public void RunCampaignAlgorithm()
        {
            var campaigns = _campaignRepository.GetCampaigns();
            foreach (var campaign in campaigns)
            {
                decimal price = 0;
                if (!IsCampaignActive(campaign))
                {
                    _campaignRepository.UpdateCampaignStatus(campaign.Name, (int)CampaignStatusEnum.Ended);
                }
                else
                {
                    var product = _productRepository.GetProduct(campaign.ProductCode);
                    decimal priceManipulationLimit = (product.Price * campaign.PriceManipulationLimit) / 100;
                    price = product.Discount + CalculateCampaignPrice(priceManipulationLimit, campaign);
                }
                _productService.UpdateProductDiscount(campaign.ProductCode, price);
            }
        }
        private bool IsCampaignActive(Campaign campaign)
        {
            if (campaign.Duration < Time.SystemTime.Hours)
            {
                return false;
            }
            return true;
        }

        private decimal CalculateCampaignPrice(decimal priceManipulationLimit, Campaign campaign)
        {
            decimal price = 0;

            int tenPercentOfTargetSalesCount = (campaign.TargetSalesCount * (int)Percent.tenPercent) / 100;
            int thirtyPercentOfTargetSalesCount = (campaign.TargetSalesCount * (int)Percent.thirtyPercent) / 100;
            int seventyPercentOfTargetSalesCount = (campaign.TargetSalesCount * (int)Percent.seventyPercent) / 100;

            if (campaign.TotalSales <= tenPercentOfTargetSalesCount)
            {
                price = -(priceManipulationLimit / campaign.Duration);
            }
            else if (campaign.TotalSales >= thirtyPercentOfTargetSalesCount && campaign.TotalSales < seventyPercentOfTargetSalesCount)
            {
                price = -(priceManipulationLimit / campaign.Duration);
            }
            else if (campaign.TotalSales >= seventyPercentOfTargetSalesCount)
            {
                price = priceManipulationLimit;
            }

            return price;
        }
    }
}
