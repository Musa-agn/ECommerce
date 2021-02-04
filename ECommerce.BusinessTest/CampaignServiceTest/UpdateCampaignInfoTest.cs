using ECommerce.BusinessLayer.Concrete;
using ECommerce.BusinessLayer.Dto.Input;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using ECommerce.Data.Request;
using Moq;
using NUnit.Framework;
using System;

namespace ECommerce.BusinessTest.CampaignServiceTest
{
    public class UpdateCampaignInfoTest
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
        public void When_UpdateCampaign_IsNull()
        {
            UpdateCampaignInfoInput input = new UpdateCampaignInfoInput
            {
                CampaignId = Guid.Empty,
                ProductCode = "P1",
                Price = 100,
                Quantity = 10
            };
            Product product = new Product("P1", 100, 1000);
            mockProductRepository.Setup(x => x.GetProduct("P1")).Returns(product);
            Campaign campaign = new Campaign("C1", "P1", 10, 20, 100);
            mockCampaignRepository.Setup(x => x.GetCampaign("C1")).Returns(campaign);

            Assert.DoesNotThrow(() => campaignService.UpdateCampaignInfo(input));

            mockCampaignRepository.Verify(x => x.UpdateCampaignInfo(It.IsAny<UpdateCampaignInfoRequest>()), Times.Never());
        }
        [Test]
        public void When_UpdateCampaignInfo_IsSuccessful()
        {
            Product product = new Product("P1", 100, 1000);
            mockProductRepository.Setup(x => x.GetProduct("P1")).Returns(product);
            Campaign campaign = new Campaign("C1", "P1", 10, 20, 100);
            mockCampaignRepository.Setup(x => x.GetCampaignById(campaign.Id)).Returns(campaign);


            UpdateCampaignInfoInput input = new UpdateCampaignInfoInput
            {
                CampaignId = campaign.Id,
                ProductCode = "P1",
                Price = 100,
                Quantity = 10
            };
            Assert.DoesNotThrow(() => campaignService.UpdateCampaignInfo(input));

            mockCampaignRepository.Verify(x => x.UpdateCampaignInfo(It.IsAny<UpdateCampaignInfoRequest>()), Times.Once());
        }
    }
}
