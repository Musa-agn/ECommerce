using ECommerce.BusinessLayer.Concrete;
using ECommerce.BusinessLayer.Dto.Input;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using Moq;
using NUnit.Framework;

namespace ECommerce.BusinessTest.CampaignServiceTest
{
    public class CreateCampaignTest
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
            CreateCampaignInput createCampaignInput = new CreateCampaignInput();
            Assert.Throws<ValidationException>(() => campaignService.CreateCampaign(createCampaignInput));
        }
        [Test]
        public void When_ProductCode_IsNull()
        {
            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
            };
            Assert.Throws<ValidationException>(() => campaignService.CreateCampaign(createCampaignInput));
        }
        [Test]
        public void When_TargetSalesCount_IsZero()
        {
            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
                ProductCode = "P1",
                TargetSalesCount = 0
            };
            Assert.Throws<InvalidTargetSaleCount>(() => campaignService.CreateCampaign(createCampaignInput));
        }
        [Test]
        public void When_TargetSalesCount_IsLessThanZero()
        {
            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
                ProductCode = "P1",
                TargetSalesCount = -10
            };
            Assert.Throws<InvalidTargetSaleCount>(() => campaignService.CreateCampaign(createCampaignInput));
        }
        [Test]
        public void When_Duration_IsZero()
        {
            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
                ProductCode = "P1",
                TargetSalesCount = 100,
                Duration = 0
            };
            Assert.Throws<InvalidDurationException>(() => campaignService.CreateCampaign(createCampaignInput));
        }
        [Test]
        public void When_Duration_IsLessThanZero()
        {
            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
                ProductCode = "P1",
                TargetSalesCount = 100,
                Duration = -10
            };
            Assert.Throws<InvalidDurationException>(() => campaignService.CreateCampaign(createCampaignInput));
        }
        [Test]
        public void When_PriceManipulationLimit_IsZero()
        {
            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
                ProductCode = "P1",
                TargetSalesCount = 100,
                Duration = 10,
                PriceManipulationLimit = 0
            };
            Assert.Throws<InvalidPriceManipulationLimitException>(() => campaignService.CreateCampaign(createCampaignInput));

        }
        [Test]
        public void When_PriceManipulationLimit_IsLessThanZero()
        {
            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
                ProductCode = "P1",
                TargetSalesCount = 100,
                Duration = 10,
                PriceManipulationLimit = -10
            };
            Assert.Throws<InvalidPriceManipulationLimitException>(() => campaignService.CreateCampaign(createCampaignInput));
        }
        [Test]
        public void When_PriceManipulationLimit_IsGreaterThanAHundred()
        {
            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
                ProductCode = "P1",
                TargetSalesCount = 100,
                Duration = 10,
                PriceManipulationLimit = 110
            };
            Assert.Throws<InvalidPriceManipulationLimitException>(() => campaignService.CreateCampaign(createCampaignInput));
        }
        [Test]
        public void When_Name_IsUsed()
        {
            Product product = new Product("P1", 100, 1000);
            mockProductRepository.Setup(x => x.GetProduct("P1")).Returns(product);
            Campaign campaign = new Campaign("C1", "P1", 10, 20, 100);
            mockCampaignRepository.Setup(x => x.GetCampaign("C1")).Returns(campaign);
            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
                ProductCode = "P1",
                TargetSalesCount = 100,
                Duration = 10,
                PriceManipulationLimit = 20
            };
            Assert.Throws<BaseServiceException>(() => campaignService.CreateCampaign(createCampaignInput));
        }

        [Test]
        public void When_CreateCampaign_IsSuccessful()
        {
            Product product = new Product("P1", 100, 1000);
            mockProductRepository.Setup(x => x.GetProduct("P1")).Returns(product);
            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
                ProductCode = "P1",
                TargetSalesCount = 100,
                Duration = 10,
                PriceManipulationLimit = 20
            };
            Assert.DoesNotThrow(() => campaignService.CreateCampaign(createCampaignInput));
            mockCampaignRepository.Verify(x => x.CreateCampaign(It.IsAny<Campaign>()), Times.Once());
        }
    }
}
