using System.IO;
using Topshelf;
using log4net.Config;

namespace Green.App.ServiceWebApi
{
    class Program
    {
        private static void Main(string[] args)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(@"Config\log4net.config"));
            HostFactory.Run(x =>
                {
                    x.Service<WebApiService>(s =>
                        {
                            s.ConstructUsing(name => new WebApiService("http://localhost/api"));
                            s.WhenStarted(tc => tc.Start());
                            s.WhenStopped(tc => tc.Stop());
                        });
                    x.RunAsLocalSystem();
                    x.SetDescription("GreenAppWebApiService");
                    x.SetDisplayName("GreenAppWebApiService");
                    x.SetServiceName("GreenAppWebApiService");
                    x.UseLog4Net();
                });
        }
    }
}
