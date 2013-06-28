﻿using System;
using System.Net.Http;
using Green.App.ServiceWebApi;
using Green.App.ServiceWebApi.WebApi;
using NUnit.Framework;

namespace Green.App.Test.Green.App.ServiceWebApi
{
    [TestFixture]
    public class TestWebApiService
    {
        private SelfHostWebApiService _selfHostWebApiService;
        private HttpClient _webApiClient;

        [SetUp]
        public void StartWebApiService()
        {
            var serviceUri = new Uri("http://localhost/testapi/");

            _selfHostWebApiService = new SelfHostWebApiService(serviceUri);
            _selfHostWebApiService.Start();

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
            _selfHostWebApiService.Stop();
        }
    }
}
