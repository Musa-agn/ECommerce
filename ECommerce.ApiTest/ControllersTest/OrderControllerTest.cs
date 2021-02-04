using ECommerce.API.Model.Request;
using ECommerce.ApiTest.StartApi;
using ECommerce.BusinessLayer.Dto.Input;
using ECommerce.BusinessLayer.Dto.Output;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ECommerce.ApiTest.ControllersTest
{
    public class OrderControllerTest
    {
        [SetUp]
        public void Setup()
        {
            TestBase.ResetData();
        }
        [Test]
        public void When_CreateOrder_IsSuccessful()
        {
            CreateProductInput createProductInput = new CreateProductInput
            {
                Code = "P1",
                Price = 100,
                Stock = 1000,
            };
            CreateProduct(createProductInput);

            CreateOrderInput createOrderInput = new CreateOrderInput
            {
                ProductCode = "P1",
                Quantity = 10
            };
            var orderResponse = CreateOrder(createOrderInput);
            Assert.AreEqual(HttpStatusCode.OK, orderResponse.StatusCode);

            var getProduct = GetProduct();
            Assert.AreEqual(990, getProduct.Stock);

            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
                ProductCode = "P1",
                TargetSalesCount = 100,
                Duration = 10,
                PriceManipulationLimit = 20
            };

            CreateCampaign(createCampaignInput);
            IncreaseTime();


            var orderResponse2 = CreateOrder(createOrderInput);
            Assert.AreEqual(HttpStatusCode.OK, orderResponse2.StatusCode);

            var getProduct2 = GetProduct();
            Assert.AreEqual(980, getProduct2.Stock);

            var campaignResponse = GetCampaign(createCampaignInput.Name);

            Assert.AreEqual(createCampaignInput.TargetSalesCount, campaignResponse.TargetSales);
            Assert.AreEqual(createOrderInput.Quantity, campaignResponse.TotalSales);
            Assert.AreEqual(980, campaignResponse.Turnover);
            Assert.AreEqual(98, campaignResponse.AverageItemPrice);
        }
        private HttpResponseMessage CreateOrder(CreateOrderInput createOrderInput)
        {
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var json = JsonConvert.SerializeObject(createOrderInput);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("/api/order", data).Result;

                return response;
            }
        }

        private void CreateProduct(CreateProductInput createProductInput)
        {
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var json = JsonConvert.SerializeObject(createProductInput);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("/api/product", data);
                Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            }
        }

        private GetProductInfoOutput GetProduct()
        {
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                string productCode = "P1";
                var response = client.GetAsync("/api/product?productCode=" + productCode);

                Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);

                var getProductInfoOutput = JsonConvert.DeserializeObject<GetProductInfoOutput>(response.Result.Content.ReadAsStringAsync().Result);

                return getProductInfoOutput;
            }
        }

        private void CreateCampaign(CreateCampaignInput createCampaignInput)
        {
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var json = JsonConvert.SerializeObject(createCampaignInput);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("/api/campaign", data).Result;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        private GetCampaignInfoOutput GetCampaign(string name)
        {
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = client.GetAsync("/api/campaign?name=" + name);

                Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);

                var getCampaignInfoOutput = JsonConvert.DeserializeObject<GetCampaignInfoOutput>(response.Result.Content.ReadAsStringAsync().Result);
                return getCampaignInfoOutput;
            }
        }

        public void IncreaseTime()
        {
            IncreaseTimeRequest increaseTimeRequest = new IncreaseTimeRequest
            {
                Hour = 1
            };
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var json = JsonConvert.SerializeObject(increaseTimeRequest);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PutAsync("/api/increasetime", data);

                Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            }
        }
    }
}
