using ECommerce.Data.Entity;
using System.Collections.Generic;

namespace ECommerce.Data
{
    public static class CampaignList
    {
        public static List<Campaign> Campaigns;
        static CampaignList()
        {
            Campaigns = new List<Campaign>();
        }
    }
}
