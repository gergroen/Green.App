using System;
using System.IO;
using System.Web.Http.SelfHost;
using log4net;

namespace Green.App.ServiceWebApi.WebServer
{
    public class SelfHostWebServer : IManageableService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(SelfHostWebServer));
        private HttpSelfHostServer _server;
        private readonly Uri _uri;
        private DirectoryInfo _webDirectory;

        public SelfHostWebServer(Uri uri, DirectoryInfo webDirectory)
        {
            _uri = uri;
            _webDirectory = webDirectory;
        }

        public void Start()
        {
            _logger.Info("Starting web server");

            var config = new HttpSelfHostConfiguration(_uri);
            config.MaxReceivedMessageSize = int.MaxValue;

            _server = new HttpSelfHostServer(config, new WebServerRequestHandler(_uri, _webDirectory));

            try
            {
                _server.OpenAsync().Wait();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }

            _logger.Info("Web server started");
            _logger.InfoFormat("Listening on {0}", _uri);
        }

        public void Stop()
        {
            _logger.Info("Stopping Web server");

            _server.CloseAsync().Wait();
            _server.Dispose();

            _logger.Info("Web server stopped");
        }
    }
}
