using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.BusinessLayer.SystemTime;
using Moq;
using NUnit.Framework;

namespace ECommerce.BusinessTest.IncreaseTimeTest
{
    public class IncreaseTimeTest
    {
        IncreaseTimeService increaseTimeService;
        Mock<ICampaignAlgorithmService> mockCampaignAlgorithmService;
        [SetUp]
        public void Setup()
        {
            mockCampaignAlgorithmService = new Mock<ICampaignAlgorithmService>();
            increaseTimeService = new IncreaseTimeService(mockCampaignAlgorithmService.Object);
        }

        [Test]
        public void When_IncreaseTime_IsSuccesful()
        {
            Assert.DoesNotThrow(() => increaseTimeService.IncreaseTime(1));
            Assert.AreEqual(1, Time.SystemTime.Hours);

            mockCampaignAlgorithmService.Verify(x => x.RunCampaignAlgorithm(), Times.Once());
        }
    }
}
