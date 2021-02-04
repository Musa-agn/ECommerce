using ECommerce.Data.Entity;
using ECommerce.Data.Request;
using System;
using System.Collections.Generic;

namespace ECommerce.Data.Abstract
{
    public interface ICampaignRepository
    {
        void CreateCampaign(Campaign campaign);
        Campaign GetCampaign(string name);
        void UpdateCampaignInfo(UpdateCampaignInfoRequest updateCampaignInfoRequest);
        void UpdateCampaignStatus(string name, int status);
        Campaign GetCampaignByProductCode(string productCode);
        List<Campaign> GetCampaigns();
        Campaign GetCampaignById(Guid id);

    }
}
