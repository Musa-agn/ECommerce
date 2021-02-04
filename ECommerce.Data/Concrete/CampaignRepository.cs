using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using ECommerce.Data.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Data.Concrete
{
    public class CampaignRepository : ICampaignRepository
    {
        public void CreateCampaign(Campaign campaign)
        {
            CampaignList.Campaigns.Add(campaign);
        }

        public Campaign GetCampaign(string name)
        {
            return CampaignList.Campaigns.FirstOrDefault(x => x.Name == name);
        }

        public Campaign GetCampaignById(Guid id)
        {
            return CampaignList.Campaigns.FirstOrDefault(x => x.Id == id);
        }

        public Campaign GetCampaignByProductCode(string productCode)
        {
            return CampaignList.Campaigns.FirstOrDefault(x => x.ProductCode == productCode);
        }

        public List<Campaign> GetCampaigns()
        {
            return CampaignList.Campaigns.ToList();
        }

        public void UpdateCampaignInfo(UpdateCampaignInfoRequest updateCampaignInfoRequest)
        {
            var campaign = CampaignList.Campaigns.FirstOrDefault(x => x.Id == updateCampaignInfoRequest.CampaignId);
            campaign.TotalSales = updateCampaignInfoRequest.TotalSales;
            campaign.Turnover = updateCampaignInfoRequest.Turnover;
            campaign.AverageItemPrice = updateCampaignInfoRequest.AverageItemPrice;
            campaign.Status = updateCampaignInfoRequest.Status;
        }

        public void UpdateCampaignStatus(string name, int status)
        {
            var campaing = CampaignList.Campaigns.FirstOrDefault(x => x.Name == name);
            campaing.Status = status;
        }
    }
}
