using System.Net;

namespace ECommerce.BusinessLayer.Exceptions
{
    public class CampaignNotFound : BaseServiceException
    {
        public CampaignNotFound() : base("Campaign not found", (int)HttpStatusCode.NotFound)
        {
        }
    }
}
