using System;
using System.IO;
using System.Net;
using RobotsExplorer.HttpManager;
using RobotsExplorer.Model;
using RobotsExplorer.ConfigManager;
using NDesk.Options;

namespace RobotsExplorer
{
    public class Program
    {
        #region Private Objects

        private static string _urlTarget = null;
        private static string _proxy = null;
        private static HttpManager.HttpManager httpManager = null;
        private static Robot robot = null;

        #endregion

        #region Public Methods

        public static void Main(string[] args)
        {
            ParseOptionsInput(args);

            if (ValidadeOptionsInput(args))
            {
                _urlTarget = "http://www.cassiobp.com.br/" + ConfigManager.ConfigManager.robotPath;

                Execute();
            }
        }

        #endregion

        #region Private Methods

        private static void ParseOptionsInput(string[] args)
        {
            OptionSet options = new OptionSet()
                .Add("u=|urlTarget=", u => _urlTarget = u)
                .Add("p|proxy=", p => _proxy = p)
                .Add("?|h|help", h => DisplayHelp());

            options.Parse(args);
        }

        private static bool ValidadeOptionsInput(string[] args)
        {
            bool valid = false;

            if (string.IsNullOrEmpty(_urlTarget))
                DisplayHelp();
            else
                valid = true;

            return valid;
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("Help :)");
        }

        private static void Execute()
        {
            Console.WriteLine("... PROCESSING ...");
            Console.WriteLine();

            Console.WriteLine("Localizing target host...");
            Console.WriteLine();

            httpManager = new HttpManager.HttpManager();
            
            WebRequest request = httpManager.WebRequestFactory(_urlTarget, _proxy, string.Empty);
            WebResponse response = request.GetResponse();

            if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                ProcessResponse((HttpWebResponse)response);
            else
            {
                Util.Util.ChangeConsoleColorToRed();
                Console.WriteLine("Sorry, I failed when I try to access the target :(");
            }
        }

        private static void AskForAttack()
        {
            string answer = string.Empty;

            do
            {
                Console.WriteLine("Do you want to exploit those directories? I am going to tell you if, they are or not listing directories! (Y/N)");

                answer = Console.ReadLine();

                if ((string.IsNullOrEmpty(answer)) || (answer.ToUpper() != "Y" && answer.ToUpper() != "N"))
                {
                    Console.WriteLine();
                    Console.WriteLine("Wrong answer ¬¬");
                }

            } while (answer.ToUpper() != "Y" && answer.ToUpper() != "N");

            if (answer.ToUpper() == "Y")
                ProceedToAttack();
            else
                FinishExecution();
        }

        private static void FinishExecution()
        {
            Util.Util.ChangeConsoleColorToDefault();
            Console.WriteLine();
            Console.WriteLine("Thank you and happy hacking ;)");
        }

        private static void ProceedToAttack()
        {
            Console.WriteLine("... PROCESSING DIRECTORY ATTACK ...");
            Console.WriteLine();

            httpManager = new HttpManager.HttpManager();

            foreach (var directory in robot.Disallows)
            {
                Util.Util.ChangeConsoleColorToDefault();
                Console.WriteLine();
                Console.WriteLine("Localizing target for directory: " + directory + " ...");
                Console.WriteLine();
                Attack(robot.Domain, directory);
            }

            FinishExecution();

            Console.Read();
        }

        private static void Attack(string domain, string directory)
        {
            try
            {
                WebRequest request = httpManager.WebRequestFactory(domain + directory, _proxy, string.Empty);
                WebResponse response = request.GetResponse();

                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    Util.Util.ChangeConsoleColorToGreen();
                    Console.WriteLine("Directory Listing enabled for directory: " + directory + " ;)");
                }
                else
                {
                    Util.Util.ChangeConsoleColorToRed();
                    Console.WriteLine("Sorry, I failed when I try to access the target directory or the directory is pretty safe :(");
                }

                Console.WriteLine();
            }
            catch (WebException ex)
            {
                if (ex.Message.Contains("(404)"))
                {
                    Util.Util.ChangeConsoleColorToRed();
                    Console.WriteLine("Sorry, this directory is no longer part of target domain. Response.HttpStatusCode = 404");
                }
                else if (ex.Message.Contains("(403)"))
                {
                    Util.Util.ChangeConsoleColorToRed();
                    Console.WriteLine("This directory is preety safe, I got a (403) response code status :(");
                }
                else
                {
                    Util.Util.ChangeConsoleColorToRed();
                    Console.WriteLine("Sorry, I failed when I try to access the target directory :(");
                }
            }
            catch (Exception ex)
            {
                Util.Util.ChangeConsoleColorToRed();
                Console.WriteLine("Sorry, I failed when I try to access the target directory or the directory is pretty safe :(");
            }
        }

        private static void ProcessResponse(HttpWebResponse response)
        {
            Util.Util.ChangeConsoleColorToGreen();
            Console.WriteLine("TARGET FOUND :)");
            Console.WriteLine();
            Util.Util.ChangeConsoleColorToDefault();
            Console.WriteLine("Getting Robots.txt...");
            Console.WriteLine();

            string robotsTxt = Util.Util.ParseResponseStreamToText(response);

            Util.Util.ChangeConsoleColorToGreen();
            Console.WriteLine("VOILÀ!");
            Console.WriteLine();
            Util.Util.ChangeConsoleColorToDefault();
            Console.WriteLine("Begin of file");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(robotsTxt);
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("End of file");
            Console.WriteLine();

            robot = Util.Util.ParseRobotTxtToRobotObject(robotsTxt, _urlTarget);

            if (robot.Disallows != null && robot.Disallows.Count > 0)
            {
                Console.WriteLine("Listing 'disallow' directories...");
                Console.WriteLine();

                foreach (var disallowDirectory in robot.Disallows)
                    Console.WriteLine(disallowDirectory);

                Console.WriteLine();

                AskForAttack();
            }
            else
            {
                Console.WriteLine("There is no 'disallow' directories on the target.");
                Console.WriteLine();
                FinishExecution();
            }
        }

        #endregion
    }
}