using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Dto;
using ECommerce.BusinessLayer.Dto.Input;
using ECommerce.BusinessLayer.Dto.Output;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using ECommerce.Data.Request;
using System;
using System.Net;

namespace ECommerce.BusinessLayer.Concrete
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        public CampaignService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }
        public void CreateCampaign(CreateCampaignInput createCampaignInput)
        {
            CreateCampaignInputValidate(createCampaignInput);
            _campaignRepository.CreateCampaign(
                new Campaign(
                    createCampaignInput.Name,
                    createCampaignInput.ProductCode,
                    createCampaignInput.Duration,
                    createCampaignInput.PriceManipulationLimit,
                    createCampaignInput.TargetSalesCount)
                );
        }

        public GetCampaignInfoOutput GetCampaignInfo(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException(nameof(name));
            }

            var campaign = _campaignRepository.GetCampaign(name);
            if (campaign == null)
            {
                throw new CampaignNotFound();
            }

            return ParseGetCampaignInfoOutput(campaign);
        }
        private GetCampaignInfoOutput ParseGetCampaignInfoOutput(Campaign campaign)
        {
            GetCampaignInfoOutput getCampaignInfoOutput = new GetCampaignInfoOutput();
            getCampaignInfoOutput.TargetSales = campaign.TargetSalesCount;
            if ((int)CampaignStatusEnum.Active == campaign.Status)
                getCampaignInfoOutput.Status = GetCampaignStatusName(CampaignStatusEnum.Active);
            else
                getCampaignInfoOutput.Status = GetCampaignStatusName(CampaignStatusEnum.Ended);

            getCampaignInfoOutput.TotalSales = campaign.TotalSales;
            getCampaignInfoOutput.Turnover = campaign.Turnover;
            getCampaignInfoOutput.AverageItemPrice = campaign.AverageItemPrice;

            return getCampaignInfoOutput;
        }
        private string GetCampaignStatusName(CampaignStatusEnum campaignStatusEnum)
        {
            return Enum.GetName(typeof(CampaignStatusEnum), campaignStatusEnum);
        }

        private void CreateCampaignInputValidate(CreateCampaignInput createCampaignInput)
        {
            if (string.IsNullOrWhiteSpace(createCampaignInput.Name))
            {
                throw new ValidationException(nameof(createCampaignInput.Name));
            }
            if (string.IsNullOrWhiteSpace(createCampaignInput.ProductCode))
            {
                throw new ValidationException(nameof(createCampaignInput.ProductCode));
            }
            if (createCampaignInput.TargetSalesCount <= 0)
            {
                throw new InvalidTargetSaleCount();
            }
            if (createCampaignInput.Duration <= 0)
            {
                throw new InvalidDurationException();
            }
            if (createCampaignInput.PriceManipulationLimit <= 0)
            {
                throw new InvalidPriceManipulationLimitException("Price manipulation limit cannot be equal to or less than zero");
            }
            if (createCampaignInput.PriceManipulationLimit > 100)
            {
                throw new InvalidPriceManipulationLimitException("Price manipulation limit cannot be higher than 100");
            }

            var campaign = _campaignRepository.GetCampaign(createCampaignInput.Name);
            if (campaign != null)
            {
                throw new BaseServiceException("This CampaignName is used.", (int)HttpStatusCode.BadRequest);
            }
        }

        public GetCampaignInfoByProductCodeOutput GetCampaignInfoByProductCode(string productCode)
        {
            var campaign = _campaignRepository.GetCampaignByProductCode(productCode);
            if (campaign == null)
            {
                return null;
            }

            return ParseGetCampaignInfoByProductCodeOutput(campaign);
        }
        private GetCampaignInfoByProductCodeOutput ParseGetCampaignInfoByProductCodeOutput(Campaign getCampaignInfoByProductCodeResponse)
        {
            GetCampaignInfoByProductCodeOutput getCampaignInfoByProductCodeOutput = new GetCampaignInfoByProductCodeOutput
            {
                Id = getCampaignInfoByProductCodeResponse.Id,
                Name = getCampaignInfoByProductCodeResponse.Name,
                TargetSales = getCampaignInfoByProductCodeResponse.TargetSalesCount,
                Status = getCampaignInfoByProductCodeResponse.Status,
                TotalSales = getCampaignInfoByProductCodeResponse.TotalSales
            };

            return getCampaignInfoByProductCodeOutput;
        }

        public void UpdateCampaignInfo(UpdateCampaignInfoInput updateCampaignInfoInput)
        {
            var campaign = _campaignRepository.GetCampaignById(updateCampaignInfoInput.CampaignId);
            if (campaign != null && campaign.Status == (int)Dto.CampaignStatusEnum.Active)
            {
                int totalSales = (updateCampaignInfoInput.Quantity + campaign.TotalSales);
                if (totalSales >= campaign.TargetSalesCount)
                {
                    UpdateCampaign(updateCampaignInfoInput, campaign, (int)CampaignStatusEnum.Ended);
                }
                else
                {
                    UpdateCampaign(updateCampaignInfoInput, campaign, (int)CampaignStatusEnum.Active);
                }
            }
        }
        private void UpdateCampaign(UpdateCampaignInfoInput updateCampaignInfoInput, Campaign campaign, int campaignStatus)
        {
            int totalsales = campaign.TotalSales + updateCampaignInfoInput.Quantity;
            decimal turnover = campaign.Turnover + updateCampaignInfoInput.Price;
            decimal averegateitemprice = turnover / totalsales;
            _campaignRepository.UpdateCampaignInfo(new UpdateCampaignInfoRequest(
                 campaign.Id,
                 totalsales,
                 turnover,
                 averegateitemprice,
                 campaignStatus
                ));
        }
    }
}
