using Green.App.ServiceWebApi;
using Green.App.Test.Helpers;
using NUnit.Framework;

namespace Green.App.Test.ServiceWebApi
{
    [TestFixture]
    public class TestWebApiService
    {
        private WebApiService _webApiService;

        [SetUp]
        public void StartWebApiService()
        {
            _webApiService = new WebApiService("http://localhost/testapi");
            _webApiService.Start();
        }

        [Test]
        public void IsOnlineTest()
        {
            Assert.AreEqual("true", HttpWebRequestHelper.CreateHttpWebRequest("http://localhost/testapi/isonline", "GET"));
        }

        [TearDown]
        public void StopWebApiService()
        {
            _webApiService.Stop();
        }
    }
}
