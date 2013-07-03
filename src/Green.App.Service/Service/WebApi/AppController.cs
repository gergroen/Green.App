using System.Web.Http;

namespace Green.App.Service.Service.WebApi
{
    [HttpHeader("Access-Control-Allow-Origin", "*")]
    public class AppController : ApiController
    {
        [HttpGet]
        public bool IsOnline()
        {
            return true;
        }
    }
}
