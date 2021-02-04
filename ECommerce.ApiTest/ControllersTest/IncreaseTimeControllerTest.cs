using ECommerce.API.Model.Request;
using ECommerce.ApiTest.StartApi;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ECommerce.ApiTest.ControllersTest
{
    public class IncreaseTimeControllerTest
    {
        [SetUp]
        public void Setup()
        {
            TestBase.ResetData();
        }
        [Test]
        public void When_IncreaseTime_IsSuccessful()
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
