using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RobotsExplorer.HttpManager
{
    public class HttpManager
    {
        public WebRequest WebRequestFactory(string Url, string proxy)
        {
            WebRequest request = WebRequest.Create(Url);
            request.Credentials = CredentialCache.DefaultCredentials;

            if (!string.IsNullOrEmpty(proxy))
                request.Proxy = Util.Util.FormatProxyStringToProxyObject(proxy);

            return request;
        }
    }
}