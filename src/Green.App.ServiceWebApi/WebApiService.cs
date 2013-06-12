using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using log4net;

namespace Green.App.ServiceWebApi
{
    public class WebApiService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(WebApiService));
        private HttpSelfHostServer _server;
        private readonly string _url;

        public WebApiService(string url)
        {
            _url = url;
        }

        public void Start()
        {
            _logger.Info("Starting service");

            var address = new Uri(_url);
            var config = new HttpSelfHostConfiguration(address);

            config.MaxReceivedMessageSize = int.MaxValue;
            config.Routes.MapHttpRoute("ActionApi", "{action}", new { controller = "App" });

            _server = new HttpSelfHostServer(config);

            try
            {
                _server.OpenAsync().Wait();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }

            _logger.Info("Service started");
            _logger.InfoFormat("Listening on {0}", _url);
        }

        public void Stop()
        {
            _logger.Info("Stopping service");

            _server.CloseAsync().Wait();
            _server.Dispose();

            _logger.Info("Service stopped");
        }
    }
}