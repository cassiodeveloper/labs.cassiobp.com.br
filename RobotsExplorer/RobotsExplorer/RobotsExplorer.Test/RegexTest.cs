using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotsExplorer.Model;

namespace RobotsExplorer.Test
{
    [TestClass]
    public class RegexTest
    {
        [TestMethod]
        public void Read_File_And_Regex_Test()
        {
            Robot robot = Util.Util.ParseRobotTxtToRobotObject(@"User-agent: *

# Sitemap
Sitemap: http://www.domain.com.br/sitemap.xml

# Teste comentário
Disallow: /teste
Disallow: /teste2
Disallow: /teste3
Disallow: /teste4
Disallow: /teste5

Allow: /

-------------------------------------------------------------
User-agent: *

Disallow: /teste
Disallow: /teste6
Disallow: /teste7
Disallow: /teste8
Disallow: /teste9

Allow: /

-------------------------------------------------------------
User-agent: Googlebot

Disallow: /teste
Disallow: /teste10
Disallow: /teste11
Disallow: /teste12
Disallow: /teste13

Allow: /

User-agent: Yahoobot

Disallow: /teste
Disallow: /teste14
Disallow: /teste15
Disallow: /teste16
Disallow: /teste17

Allow: /

-------------------------------------------------------------
User-agent: *

Allow: /

Disallow: /teste
Disallow: /teste18
Disallow: /teste19
Disallow: /teste20
Disallow: /teste21", "www.teste.com.br");
            Assert.IsNotNull(robot);
        }
    }
}