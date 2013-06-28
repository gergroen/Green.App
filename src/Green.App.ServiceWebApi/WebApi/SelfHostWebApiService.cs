using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using log4net;

namespace Green.App.ServiceWebApi.WebApi
{
    public class SelfHostWebApiService : IManageableService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(SelfHostWebApiService));
        private HttpSelfHostServer _server;
        private readonly Uri _uri;

        public SelfHostWebApiService(Uri uri)
        {
            _uri = uri;
        }

        public void Start()
        {
            _logger.Info("Starting api service");

            var config = new HttpSelfHostConfiguration(_uri);
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

            _logger.Info("Api service started");
            _logger.InfoFormat("Listening on {0}", _uri);
        }

        public void Stop()
        {
            _logger.Info("Stopping api service");

            _server.CloseAsync().Wait();
            _server.Dispose();

            _logger.Info("Api service stopped");
        }
    }
}