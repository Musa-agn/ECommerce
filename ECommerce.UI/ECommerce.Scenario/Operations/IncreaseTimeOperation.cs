using ECommerce.Scenario.BaseClient;
using ECommerce.Scenario.Model.Request;
using System;

namespace ECommerce.Scenario.Operations
{
    public class IncreaseTimeOperation
    {
        private readonly RestClientHelper _restClientHelper;
        public IncreaseTimeOperation()
        {
            _restClientHelper = new RestClientHelper();
        }
        public void IncreaseTime(string[] operationParameters)
        {
            try
            {
                int hour = 0;
                if (!int.TryParse(operationParameters[1], out hour))
                    throw new FormatException("Hour format is wrong");

                var response = _restClientHelper.IncreaseTime(
                    new IncreaseTimeRequest
                    {
                        Hour = hour
                    });
                var isError = response.Item1;
                if (isError)
                    Console.WriteLine(response.Item2);
                else
                {
                    Console.WriteLine($"Time is {response.Item2.PadLeft(2, '0')}:00");
                }
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
    }
}
