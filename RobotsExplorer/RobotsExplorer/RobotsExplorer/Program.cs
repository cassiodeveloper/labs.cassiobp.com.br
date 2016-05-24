using System;
using System.IO;
using System.Net;
using RobotsExplorer.HttpManager;
using RobotsExplorer.Model;
using RobotsExplorer.ConfigManager;

namespace RobotsExplorer
{
    public class Program
    {
        #region Private Objects

        static string urlTargetTest = string.Empty;
        static string proxyTest = "http:mtzcpd1053:Cvc2016@:cvcproxy01.cvc.com.br:8080";

        #endregion

        #region Public Methods

        public static void Main(string[] args)
        {
            urlTargetTest = "http://www.cassiobp.com.br/" + ConfigManager.ConfigManager.robotPath;
            Execute();
        }

        #endregion

        #region Private Methods

        private static void Execute()
        {
            Console.WriteLine("... PROCESSING ...");
            Console.WriteLine();

            Console.WriteLine("Localizing target host...");
            Console.WriteLine();

            HttpManager.HttpManager httpManager = new HttpManager.HttpManager();
            
            WebRequest request = httpManager.WebRequestFactory(urlTargetTest, proxyTest, string.Empty);
            WebResponse response = request.GetResponse();

            if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                ProcessResponse((HttpWebResponse)response);
            else
                Console.WriteLine("Sorry, I failed when I try to access the target :(");

            Console.Read();
        }

        private static void ProcessResponse(HttpWebResponse response)
        {
            Console.WriteLine("TARGET FOUND :)");
            Console.WriteLine();
            Console.WriteLine("Getting Robots.txt...");
            Console.WriteLine();

            string robotsTxt = Util.Util.ParseResponseStreamToText(response);

            Console.WriteLine("VOILÀ!");
            Console.WriteLine();
            Console.WriteLine("Begin of file");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(robotsTxt);
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("End of file");
            Console.WriteLine();

            Robot robot = Util.Util.ParseRobotTxtToRobotObject(robotsTxt, urlTargetTest);

            Console.WriteLine("Listing 'disallow' directories...");
            Console.WriteLine();

            foreach (var disallowDirectory in robot.Disallows)
                Console.WriteLine(disallowDirectory);
        }

        #endregion
    }
}