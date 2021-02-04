using ECommerce.BusinessLayer.Concrete;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using Moq;
using NUnit.Framework;

namespace ECommerce.BusinessTest.CampaignServiceTest
{
    public class GetCampaignInfoTest
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
        public void When_Name_IsNull()
        {
            Assert.Throws<ValidationException>(() => campaignService.GetCampaignInfo(null));
        }
        [Test]
        public void When_Campaign_NotFound()
        {
            Assert.Throws<CampaignNotFound>(() => campaignService.GetCampaignInfo("C1"));
        }
        [Test]
        public void When_GetCampaignInfo_IsSuccessful()
        {
            Product product = new Product("P1", 100, 1000);
            mockProductRepository.Setup(x => x.GetProduct("P1")).Returns(product);
            Campaign campaign = new Campaign("C1", "P1", 10, 20, 100);
            mockCampaignRepository.Setup(x => x.GetCampaign("C1")).Returns(campaign);


            var response = campaignService.GetCampaignInfo("C1");

            Assert.AreEqual(campaign.TargetSalesCount, response.TargetSales);
            Assert.AreEqual(0, response.TotalSales);
            Assert.AreEqual(0, response.Turnover);
            Assert.AreEqual(0, response.AverageItemPrice);
            Assert.AreEqual("Active", response.Status);

            mockCampaignRepository.Verify(x => x.GetCampaign(It.IsAny<string>()), Times.Once());
        }

    }
}
