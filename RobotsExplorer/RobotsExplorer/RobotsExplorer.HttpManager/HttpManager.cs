using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace RobotsExplorer.HttpManager
{
    public class HttpManager
    {
        public WebRequest WebRequestFactory(string Url, string proxy, string userAgent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.UserAgent = userAgent;

            if (!string.IsNullOrEmpty(proxy))
                request.Proxy = Util.Util.FormatProxyStringToProxyObject(proxy);

            return request;
        }
    }
}