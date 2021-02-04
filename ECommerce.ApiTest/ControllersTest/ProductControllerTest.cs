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
    public class ProductControllerTest
    {
        [SetUp]
        public void Setup()
        {
            TestBase.ResetData();
        }
        [Test]
        public void When_CreateProduct_IsSuccessful()
        {
            using (var client = new TestStart().Client)
            {
                CreateProductRequest input = new CreateProductRequest
                {
                    Code = "P1",
                    Price = 100,
                    Stock = 1000
                };

                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var json = JsonConvert.SerializeObject(input);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("/api/product", data);
                Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            }
        }

        [Test]
        public void When_GetProductInfo_IsSuccessful()
        {
            CreateProductRequest input = new CreateProductRequest
            {
                Code = "P1",
                Price = 100,
                Stock = 1000,
            };
            CreateProduct(input);

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

            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                string productCode = "P1";
                var response = client.GetAsync("/api/product?productCode=" + productCode);

                Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);

                var getProductInfoOutput = JsonConvert.DeserializeObject<GetProductInfoOutput>(response.Result.Content.ReadAsStringAsync().Result);
                Assert.IsNotNull(getProductInfoOutput);

                Assert.AreNotEqual(input.Price, getProductInfoOutput.Price);
                Assert.AreEqual(input.Stock, getProductInfoOutput.Stock);
            }
        }

        private void CreateProduct(CreateProductRequest input)
        {
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var json = JsonConvert.SerializeObject(input);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("/api/product", data);
                Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            }
        }
        private void CreateCampaign(CreateCampaignInput createCampaignInput)
        {
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var json = JsonConvert.SerializeObject(createCampaignInput);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("/api/campaign", data);
                Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
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
