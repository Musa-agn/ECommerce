using ECommerce.BusinessLayer.Concrete;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using Moq;
using NUnit.Framework;

namespace ECommerce.BusinessTest.CampaignServiceTest
{
    public class GetCampaignInfoByProductCodeTest
    {
        CampaignService campaignService;
        Mock<ICampaignRepository> mockCampaignRepository;
        Mock<IProductRepository> mockProductRepository;
        [SetUp]
        public void Setup()
        {
            mockCampaignRepository = new Mock<ICampaignRepository>();
            mockProductRepository = new Mock<IProductRepository>();
            campaignService = new CampaignService(mockCampaignRepository.Object);
        }

        [Test]
        public void When_GetCampaignInfoByProductCode_IsNull()
        {

            var response = campaignService.GetCampaignInfoByProductCode("C1");

            Assert.IsNull(response);

        }
        [Test]
        public void When_GetCampaignInfoByProductCode_IsSuccessful()
        {
            Product product = new Product("P1", 100, 1000);
            mockProductRepository.Setup(x => x.GetProduct("P1")).Returns(product);
            Campaign campaign = new Campaign("C1", "P1", 10, 20, 100);
            mockCampaignRepository.Setup(x => x.GetCampaignByProductCode(product.Code)).Returns(campaign);

            var response = campaignService.GetCampaignInfoByProductCode(product.Code);

            Assert.AreEqual(campaign.Name, response.Name);
            Assert.AreEqual(campaign.TargetSalesCount, response.TargetSales);
            Assert.AreEqual(campaign.TotalSales, response.TotalSales);
        }
    }
}
