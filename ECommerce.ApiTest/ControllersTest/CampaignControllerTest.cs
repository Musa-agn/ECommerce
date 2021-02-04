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
    public class CampaignControllerTest
    {
        [SetUp]
        public void Setup()
        {
            TestBase.ResetData();
        }
        [Test]
        public void When_CreateCampaign_IsSuccessful()
        {
            CreateProduct();

            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
                ProductCode = "P1",
                TargetSalesCount = 100,
                Duration = 10,
                PriceManipulationLimit = 20
            };

            var response = CreateCampaign(createCampaignInput);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void When_GetCampaignInfo_IsSuccessful()
        {
            CreateProduct();

            CreateCampaignInput createCampaignInput = new CreateCampaignInput
            {
                Name = "C1",
                ProductCode = "P1",
                TargetSalesCount = 100,
                Duration = 10,
                PriceManipulationLimit = 20
            };
            CreateCampaign(createCampaignInput);
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                string name = "C1";
                var response = client.GetAsync("/api/campaign?name=" + name);

                Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);

                var getCampaignInfoOutput = JsonConvert.DeserializeObject<GetCampaignInfoOutput>(response.Result.Content.ReadAsStringAsync().Result);

                Assert.IsNotNull(getCampaignInfoOutput);
                Assert.AreEqual(0, getCampaignInfoOutput.TotalSales);
                Assert.AreEqual(createCampaignInput.TargetSalesCount, getCampaignInfoOutput.TargetSales);
                Assert.AreEqual(0, getCampaignInfoOutput.Turnover);
            }
        }
        private void CreateProduct()
        {
            CreateProductInput createProductInput = new CreateProductInput
            {
                Code = "P1",
                Price = 100,
                Stock = 1000,
            };
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var json = JsonConvert.SerializeObject(createProductInput);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("/api/product", data);
                Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            }
        }
        private HttpResponseMessage CreateCampaign(CreateCampaignInput createCampaignInput)
        {
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var json = JsonConvert.SerializeObject(createCampaignInput);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync("/api/campaign", data).Result;
                return response;
            }
        }
    }
}
