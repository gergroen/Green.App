using System.Web.Http;

namespace Green.App.ServiceWebApi
{
    public class AppController : ApiController
    {
        [HttpGet]
        public bool IsOnline()
        {
            return true;
        }
    }
}
