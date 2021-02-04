using ECommerce.Scenario.BaseClient;
using ECommerce.Scenario.Model.Request;
using System;

namespace ECommerce.Scenario.Operations
{
    public class ProductOperation
    {
        private readonly RestClientHelper _restClientHelper;
        public ProductOperation()
        {
            _restClientHelper = new RestClientHelper();
        }
        public void CreateProduct(string[] operationParamaters)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(operationParamaters[1]))
                    throw new ArgumentNullException("Code is Invalid");
                decimal price = 0;
                if (!decimal.TryParse(operationParamaters[2], out price))
                    throw new FormatException("Price format is wrong");
                int stock = 0;
                if (!int.TryParse(operationParamaters[3], out stock))
                    throw new FormatException("Stock format is wrong");

                var response = _restClientHelper.CreateProduct(new CreateProductRequest
                {
                    Code = operationParamaters[1],
                    Price = price,
                    Stock = stock
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
        public void GetProductInfo(string[] operationParameters)
        {
            try
            {
                if (operationParameters.Length == 2)
                {
                    if (string.IsNullOrWhiteSpace(operationParameters[1]))
                        throw new ArgumentNullException();

                    string response = _restClientHelper.GetProductInfo(operationParameters[1]);
                    Console.WriteLine(response);
                }
                else
                    throw new ArgumentNullException();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("ProductCode is Invalid");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
