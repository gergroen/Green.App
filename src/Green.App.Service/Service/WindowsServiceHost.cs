using System.Collections.Generic;
using System.Linq;

namespace Green.App.ServiceWebApi
{
    public class WindowsServiceHost
    {
        private readonly IList<IManageableService> _services = new List<IManageableService>();

        public WindowsServiceHost(params IManageableService[] services)
        {
            _services = services.ToList();
        }

        public void Start()
        {
            foreach (var service in _services)
            {
                service.Start();
            }
        }

        public void Stop()
        {
            foreach (var service in _services.Reverse())
            {
                service.Stop();
            }
        }
    }
}
