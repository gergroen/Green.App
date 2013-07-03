using System.Web.Http.Filters;

namespace Green.App.ServiceWebApi.WebApi
{
    public class HttpHeaderAttribute : ActionFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        /// <summary>
        /// Set Http Headers in the response
        /// </summary>
        /// <param name="name">Name of the Http header</param>
        /// <param name="value">Value of the Http header</param>
        public HttpHeaderAttribute(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
            {
                actionExecutedContext.Response.Headers.Add(_name, _value);
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}