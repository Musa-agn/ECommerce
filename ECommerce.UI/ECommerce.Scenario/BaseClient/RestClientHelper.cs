using ECommerce.Scenario.Model.Request;
using ECommerce.Scenario.Model.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System.Configuration;

namespace ECommerce.Scenario.BaseClient
{
    public class RestClientHelper
    {
        private readonly string _baseUrl;
        public RestClientHelper()
        {
            _baseUrl = ConfigurationManager.AppSettings["ecommerceAPIUrl"];

        }
        JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        public string CreateProduct(CreateProductRequest createProductRequest)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("api/product", Method.POST);
            request.AddHeader("Accept", "application/json");
            string jsonObject = JsonConvert.SerializeObject(createProductRequest, Formatting.Indented, jsonSerializerSettings);
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return response.Content;

            return $"Product created; code {createProductRequest.Code}, price {createProductRequest.Price}, stock {createProductRequest.Stock}";
        }

        public string CreateCampaign(CreateCampaignRequest createCampaignRequest)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("api/campaign", Method.POST);
            request.AddHeader("Accept", "application/json");
            string jsonObject = JsonConvert.SerializeObject(createCampaignRequest, Formatting.Indented, jsonSerializerSettings);
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return response.Content;
            return $"Campaign created; name {createCampaignRequest.Name}, product {createCampaignRequest.ProductCode}, " +
                    $"duration {createCampaignRequest.Duration}, limit {createCampaignRequest.PriceManipulationLimit}, target sales count {createCampaignRequest.TargetSalesCount}";
        }
        public string CreateOrder(CreateOrderRequest createOrderRequest)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("api/order", Method.POST);
            request.AddHeader("Accept", "application/json");
            string jsonObject = JsonConvert.SerializeObject(createOrderRequest, Formatting.Indented, jsonSerializerSettings);
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return response.Content;
            }
            return $"Order created; product {createOrderRequest.ProductCode}, quantity {createOrderRequest.Quantity}";
        }
        public (bool, string) IncreaseTime(IncreaseTimeRequest increaseTimeRequest)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("api/increasetime", Method.PUT);
            request.AddHeader("Accept", "application/json");
            string jsonObject = JsonConvert.SerializeObject(increaseTimeRequest, Formatting.Indented, jsonSerializerSettings);
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return (true, response.Content);
            }
            var increaseTimeResponse = JsonConvert.DeserializeObject<IncreaseTimeResponse>(response.Content);
            return (false, increaseTimeResponse.Hour.ToString());
        }
        public string GetProductInfo(string productCode)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("api/product", Method.GET, DataFormat.Json);
            request.AddParameter("productCode", productCode);
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return response.Content;

            var getProductInfoResponse = JsonConvert.DeserializeObject<GetProductInfoResponse>(response.Content);
            return $"Product {productCode} info; price {getProductInfoResponse.Price}, stock {getProductInfoResponse.Stock}";
        }
        public string GetCampaignInfo(string name)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("api/campaign", Method.GET, DataFormat.Json);
            request.AddParameter("name", name);
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return response.Content;

            var getCampaignInfoResponse = JsonConvert.DeserializeObject<GetCampaignInfoResponse>(response.Content);
            return $"Campaign {name} info; Status {getCampaignInfoResponse.Status}," +
           $" Target Sales {getCampaignInfoResponse.TargetSales}, Total Sales {getCampaignInfoResponse.TotalSales}," +
           $" Turnover {getCampaignInfoResponse.Turnover}, Average Item Price {getCampaignInfoResponse.AverageItemPrice}";
        }
        public void ResetData()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("api/scenario", Method.DELETE);
            request.AddHeader("Accept", "application/json");
            string jsonObject = JsonConvert.SerializeObject(Formatting.Indented, jsonSerializerSettings);
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
            }
        }
    }
}
