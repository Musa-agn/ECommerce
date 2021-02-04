using ECommerce.Scenario.BaseClient;
using ECommerce.Scenario.Model.Request;
using System;

namespace ECommerce.Scenario.Operations
{
    public class OrderOperation
    {
        private readonly RestClientHelper _restClientHelper;
        public OrderOperation()
        {
            _restClientHelper = new RestClientHelper();
        }
        public void CreateOrder(string[] operationParameters)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(operationParameters[1]))
                    throw new ArgumentNullException("ProductCode is Invalid");
                int quantity = 0;
                if (!int.TryParse(operationParameters[2], out quantity))
                    throw new FormatException("Quantity format is wrong");

                var response = _restClientHelper.CreateOrder(new CreateOrderRequest
                {
                    ProductCode = operationParameters[1],
                    Quantity = quantity
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
    }
}
