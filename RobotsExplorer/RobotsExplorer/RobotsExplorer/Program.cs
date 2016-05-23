using System;
using System.IO;
using System.Net;
using RobotsExplorer.HttpManager;
using RobotsExplorer.Model;

namespace RobotsExplorer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Execute();
        }

        private static void Execute()
        {
            Console.WriteLine("... PROCESSING ...");
            Console.WriteLine();

            string urlTargetTest = "http://www.cassiobp.com.br/robots.txt";
            string proxyTest = "http:mtzcpd1053:Cvc2016@:cvcproxy01.cvc.com.br:8080";

            Console.WriteLine("Localizing target host...");
            Console.WriteLine();

            HttpManager.HttpManager httpManager = new HttpManager.HttpManager();
            
            WebRequest request = httpManager.WebRequestFactory(urlTargetTest, proxyTest, string.Empty);
            WebResponse response = request.GetResponse();

            if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("TARGET FOUND :)");
                Console.WriteLine();
                Console.WriteLine("Getting Robots.txt...");
                Console.WriteLine();
            }

            string robotsTxt = Util.Util.ParseResponseStreamToText(response);

            Console.WriteLine("VOILÀ!");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(robotsTxt);
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");

            Robot robot = Util.Util.ParseRobotTxtToRobotObject(robotsTxt);
            
            Console.Read();
        }
    }
}