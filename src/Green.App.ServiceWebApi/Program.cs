using Topshelf;
using log4net.Config;

namespace Green.App.ServiceWebApi
{
    class Program
    {
        private static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            HostFactory.Run(x =>
                {
                    x.Service<WebApiService>(s =>
                        {
                            s.ConstructUsing(name => new WebApiService());
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
