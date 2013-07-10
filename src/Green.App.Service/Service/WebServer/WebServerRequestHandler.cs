using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using log4net;

namespace Green.App.Service.WebServer
{
    public class WebServerRequestHandler : WebRequestHandler
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(WebServerRequestHandler));
        private DirectoryInfo _webDirectory;
        private Uri _baseUri;

        public WebServerRequestHandler(Uri baseUri, DirectoryInfo webDirectory)
        {
            _baseUri = baseUri;
            _webDirectory = webDirectory;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return AsyncProcessWebServerRequest(request);
        }

        private async Task<HttpResponseMessage> AsyncProcessWebServerRequest(HttpRequestMessage request)
        {
            return ProcessWebServerRequest(request);
        }

        private HttpResponseMessage ProcessWebServerRequest(HttpRequestMessage request)
        {
            _logger.InfoFormat("Request {0}", request.RequestUri);

            var requestUrl = request.RequestUri.LocalPath;
            var file = requestUrl.Replace("/www/", "");

            var filePath = _webDirectory + file;
            if (string.IsNullOrWhiteSpace(file))
            {
                filePath = _webDirectory + "index.html";
            }

            var fileInfo = new FileInfo(filePath);
            var response = new HttpResponseMessage();
            response.Content = new StreamContent(fileInfo.OpenRead());
            if (fileInfo.Extension.ToLower() == ".html")
            {
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            }
            else if (fileInfo.Extension.ToLower() == ".css")
            {
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/css");
            }
            else if (fileInfo.Extension.ToLower() == ".js")
            {
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/javascript");
            }
            else if (fileInfo.Extension.ToLower() == ".gif")
            {
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");
            }
            else
            {
                throw new NotSupportedException("Media type is not supported");
            }
            return response;
        }
    }
}