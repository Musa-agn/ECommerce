using ECommerce.BusinessLayer.Dto.Input;
using ECommerce.BusinessLayer.Dto.Output;

namespace ECommerce.BusinessLayer.Abstract
{
    public interface ICampaignService
    {
        void CreateCampaign(CreateCampaignInput createCampaignInput);
        GetCampaignInfoOutput GetCampaignInfo(string name);
        void UpdateCampaignInfo(UpdateCampaignInfoInput updateCampaignInfoInput);
        GetCampaignInfoByProductCodeOutput GetCampaignInfoByProductCode(string productCode);
    }
}
