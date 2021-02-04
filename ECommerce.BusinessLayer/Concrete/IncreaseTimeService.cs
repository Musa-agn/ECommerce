using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Dto.Output;
using ECommerce.BusinessLayer.SystemTime;
using System;

namespace ECommerce.BusinessLayer.Concrete
{
    public class IncreaseTimeService : IIncreaseTimeService
    {
        private readonly ICampaignAlgorithmService _campaignAlgorithm;
        public IncreaseTimeService(ICampaignAlgorithmService campaignAlgorithm)
        {
            _campaignAlgorithm = campaignAlgorithm;
        }
        public IncreaseTimeOutput IncreaseTime(int hour)
        {
            Time.SystemTime = new TimeSpan(Time.SystemTime.Hours + hour, 0, 0);
            _campaignAlgorithm.RunCampaignAlgorithm();
            return new IncreaseTimeOutput
            {
                Hour = Time.SystemTime.Hours
            };
        }
    }
}
