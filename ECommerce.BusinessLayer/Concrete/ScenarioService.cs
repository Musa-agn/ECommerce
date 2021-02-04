using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.SystemTime;
using ECommerce.Data;
using System;

namespace ECommerce.BusinessLayer.Concrete
{
    public class ScenarioService : IScenarioService
    {
        public void ResetData()
        {
            ProductList.Products.Clear();
            CampaignList.Campaigns.Clear();
            OrderList.Orders.Clear();
            Time.SystemTime = new TimeSpan(0, 0, 0);
        }
    }
}
