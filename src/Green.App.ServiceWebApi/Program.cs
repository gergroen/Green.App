using System;
using System.IO;
using Green.App.ServiceWebApi.WebApi;
using Green.App.ServiceWebApi.WebServer;
using Topshelf;
using log4net.Config;

namespace Green.App.ServiceWebApi
{
    class Program
    {
        private static void Main(string[] args)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(@"Config\log4net.config"));

            var webApiUri = new Uri("http://localhost/api/");
            var webServerUri = new Uri("http://localhost/www/");
            var webServerFileDir = @"C:\Data\Development\Source\Microsoft.Net\Green.App\src\Green.App\";

            var webApiService = new SelfHostWebApiService(webApiUri);
            var webServer = new SelfHostWebServer(webServerUri, webServerFileDir);

            HostFactory.Run(x =>
                {
                    x.Service<WindowsServiceHost>(s =>
                        {
                            s.ConstructUsing(name => new WindowsServiceHost(webApiService, webServer));
                            s.WhenStarted(tc => tc.Start());
                            s.WhenStopped(tc => tc.Stop());
                        });
                    x.RunAsLocalSystem();
                    x.SetServiceName("GreenAppService");
                    x.SetDisplayName("Green App Service");
                    x.SetDescription("This service host the webapi and the webservice");
                    x.UseLog4Net();
                });
        }
    }
}
