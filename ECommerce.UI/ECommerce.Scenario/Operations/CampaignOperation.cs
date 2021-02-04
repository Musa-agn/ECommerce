using ECommerce.Scenario.BaseClient;
using ECommerce.Scenario.Model.Request;
using System;

namespace ECommerce.Scenario.Operations
{
    public class CampaignOperation
    {
        private readonly RestClientHelper _restClientHelper;
        public CampaignOperation()
        {
            _restClientHelper = new RestClientHelper();
        }
        public void CreateCampaign(string[] operationParameters)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(operationParameters[1]))
                    throw new ArgumentNullException("Name is invalid");
                if (string.IsNullOrWhiteSpace(operationParameters[2]))
                    throw new ArgumentNullException("ProductCode is invalid");

                int duration = 0;
                if (!int.TryParse(operationParameters[3], out duration))
                    throw new FormatException("Duration format is wrong");

                int priceManipulationLimit = 0;
                if (!int.TryParse(operationParameters[4], out priceManipulationLimit))
                    throw new FormatException("PriceManipulationLimit format is wrong");

                int targetSalesCount = 0;
                if (!int.TryParse(operationParameters[5], out targetSalesCount))
                    throw new FormatException("TargetSalesCount format is wrong");


                var response = _restClientHelper.CreateCampaign(new CreateCampaignRequest
                {
                    Name = operationParameters[1],
                    ProductCode = operationParameters[2],
                    Duration = duration,
                    PriceManipulationLimit = priceManipulationLimit,
                    TargetSalesCount = targetSalesCount
                });

                Console.WriteLine(response);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void GetCampaignInfo(string[] operationParameters)
        {
            try
            {
                if (operationParameters.Length == 2)
                {
                    if (string.IsNullOrWhiteSpace(operationParameters[1]))
                        throw new ArgumentNullException();

                    string response = _restClientHelper.GetCampaignInfo(operationParameters[1]);
                    Console.WriteLine(response);
                }
                else
                    throw new ArgumentNullException();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Name is Invalid");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
