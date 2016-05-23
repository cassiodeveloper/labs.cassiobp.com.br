using RobotsExplorer.Model;
using System;
using System.IO;
using System.Net;

namespace RobotsExplorer.Util
{
    public static class Util
    {
        public static IWebProxy FormatProxyStringToProxyObject(string proxyPattern)
        {
            if(string.IsNullOrEmpty(proxyPattern))
                return null;

            WebProxy webProxy = new WebProxy();
            Proxy proxy = new Proxy();

            try
            {
                string[] proxyData = proxyPattern.Split(':');

                proxy.Protocol = proxyData[0];
                proxy.User = proxyData[1];
                proxy.Password = proxyData[2];
                proxy.Domain = proxyData[3];
                proxy.Port = Convert.ToInt32(proxyData[4]);

                Uri proxyUri = new Uri(proxy.Protocol + "://" + proxy.Domain + ":" + proxy.Port);

                webProxy.Address = proxyUri;
                webProxy.Credentials = new NetworkCredential(proxy.User, proxy.Password);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when I try to format proxy string. :(", ex);
            }

            return webProxy;
        }

        public static string ParseResponseStreamToText(WebResponse response)
        {
            try
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                
                string robotsTxt = reader.ReadToEnd();
                
                response.Close();
                reader.Close();
                
                return robotsTxt;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while I try to read Response Stream :(", ex);
            }
        }
    }
}