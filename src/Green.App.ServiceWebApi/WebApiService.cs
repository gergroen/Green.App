using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using log4net;

namespace Green.App.ServiceWebApi
{
    public class WebApiService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(WebApiService));

        public string Url { get; set; }
        public int MaxReceivedMessageSize { get; set; }

        private HttpSelfHostServer _server;

        public WebApiService()
        {
            Url = "http://localhost/Default";
            MaxReceivedMessageSize = int.MaxValue;
        }

        public void Start()
        {
            _logger.Info("Starting service");

            var address = new Uri(Url);
            var config = new HttpSelfHostConfiguration(address);

            config.MaxReceivedMessageSize = MaxReceivedMessageSize;
            config.Routes.MapHttpRoute("ActionApi", "{controller}/{action}");

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
            _logger.InfoFormat("Listening on {0}", Url);
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