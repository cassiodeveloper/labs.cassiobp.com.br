using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotsExplorer.Model;
using RobotsExplorer.Util;
using System.Net;

namespace RobotsExplorer.Test
{
    [TestClass]
    public class ProxyHelperTest
    {
        [TestMethod]
        public void Proxy_String_Parser_Test()
        {
            IWebProxy proxy = Util.Util.FormatProxyStringToProxyObject("http:teste:123456:dominio.com.br:8080");
            Assert.IsNotNull(proxy);
        }
    }
}