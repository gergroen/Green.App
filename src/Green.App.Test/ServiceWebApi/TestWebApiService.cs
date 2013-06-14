using System;
using System.Net.Http;
using Green.App.ServiceWebApi;
using NUnit.Framework;

namespace Green.App.Test.ServiceWebApi
{
    [TestFixture]
    public class TestWebApiService
    {
        private WebApiService _webApiService;
        private HttpClient _webApiClient;

        [SetUp]
        public void StartWebApiService()
        {
            var serviceUri = new Uri("http://localhost/testapi/");

            _webApiService = new WebApiService(serviceUri);
            _webApiService.Start();

            _webApiClient= new HttpClient();
            _webApiClient.BaseAddress = serviceUri;
        }

        [Test]
        public void IsOnlineTest()
        {
            var response = _webApiClient.GetAsync("isonline").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsTrue(response.Content.ReadAsAsync<bool>().Result);
        }

        [TearDown]
        public void StopWebApiService()
        {
            _webApiClient.Dispose();
            _webApiService.Stop();
        }
    }
}
