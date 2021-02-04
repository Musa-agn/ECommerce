using ECommerce.BusinessLayer.Concrete;
using ECommerce.BusinessLayer.SystemTime;
using ECommerce.Data;
using NUnit.Framework;
using System;

namespace ECommerce.BusinessTest.ScenarioTest
{
    public class ResetDataTest
    {
        ScenarioService scenarioService;
        [SetUp]
        public void Setup()
        {
            scenarioService = new ScenarioService();
        }
        [Test]
        public void ResetData()
        {
            ProductList.Products.Add(new Data.Entity.Product("P1", 100, 1000));
            OrderList.Orders.Add(new Data.Entity.Order("P1", 1, 100, null));
            CampaignList.Campaigns.Add(new Data.Entity.Campaign("C1", "P1", 5, 20, 100));
            Time.SystemTime = new TimeSpan(2, 0, 0);

            Assert.DoesNotThrow(() => scenarioService.ResetData());

            Assert.IsTrue(ProductList.Products.Count == 0);
            Assert.IsTrue(OrderList.Orders.Count == 0);
            Assert.IsTrue(CampaignList.Campaigns.Count == 0);
            Assert.IsTrue(Time.SystemTime.Hours == 0);
        }
    }
}
