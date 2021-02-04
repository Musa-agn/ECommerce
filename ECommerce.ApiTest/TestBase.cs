using ECommerce.ApiTest.StartApi;

namespace ECommerce.ApiTest
{
    public class TestBase
    {
        public static void ResetData()
        {
            using (var client = new TestStart().Client)
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = client.DeleteAsync("/api/scenario");
            }
        }
    }
}
