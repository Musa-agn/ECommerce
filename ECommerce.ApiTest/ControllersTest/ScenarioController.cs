using ECommerce.ApiTest.StartApi;
using NUnit.Framework;
using System.Net;

namespace ECommerce.ApiTest.ControllersTest
{
    public class ScenarioController
    {
        [Test]
        public void When_ResetData_IsSuccessful()
        {
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = client.DeleteAsync("/api/scenario");

                Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            }
        }
    }
}
