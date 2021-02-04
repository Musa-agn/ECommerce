using ECommerce.API;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace ECommerce.ApiTest.StartApi
{
    public class TestStart
    {
        private TestServer _server;
        public HttpClient Client { get; private set; }

        public TestStart()
        {
            SetUpClient();
        }
        private void SetUpClient()
        {

            _server = new TestServer(GetWebHostBuilder());

            Client = _server.CreateClient();
        }

        private IWebHostBuilder GetWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                   .UseStartup<Startup>()
                   .UseEnvironment("Test");
        }
    }
}
