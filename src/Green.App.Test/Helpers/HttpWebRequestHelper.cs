using System.IO;
using System.Net;

namespace Green.App.Test.Helpers
{
    public static class HttpWebRequestHelper
    {
        public static string CreateHttpWebRequest(string url, string method)
        {
            string result;
            var req = WebRequest.CreateHttp(url);
            req.Method = method;
            using (var response = req.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                        reader.Close();
                    }
                    stream.Close();
                }
                response.Close();
            }
            return result;
        }
    }
}