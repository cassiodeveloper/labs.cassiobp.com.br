using RobotsExplorer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace RobotsExplorer.Util
{
    public static class Util
    {
        #region Public Methods

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

        public static Robot ParseRobotTxtToRobotObject(string robotTxt, string urlTargetTest)
        {
            Robot robot = new Robot();

            robot.Domain = urlTargetTest.ToLower().Replace("/robots.txt", string.Empty);
            
            try
            {
                var matchUserAgent = GetRegexMatches(robotTxt, "User-agent: (.+)", RegexOptions.None);
                var matchDisallow = GetRegexMatches(robotTxt, "Disallow: (.+)", RegexOptions.None);
                var matchAllow = GetRegexMatches(robotTxt, "Allow: (.+)", RegexOptions.None);
                var matchComments = GetRegexMatches(robotTxt, "# (.+)", RegexOptions.None);
                var matchSiteMap = GetRegexMatches(robotTxt, "Sitemap: (.+)", RegexOptions.None);

                ParseUserAgentData(matchUserAgent, robot);
                ParseDisallowData(matchDisallow, robot);
                ParseAllowData(matchAllow, robot);
                ParseCommentsData(matchComments, robot);
                ParseSiteMapData(matchSiteMap, robot);
            }
            catch (Exception ex)
            {
                robot = null;
                throw new Exception("An error occurred when I try to parse Robot object", ex);
            }

            return robot;
        }

        #endregion

        #region Private and Auxiliary Methods

        private static void ParseSiteMapData(MatchCollection matchSiteMap, Robot robot)
        {
            if (matchSiteMap != null && matchSiteMap.Count > 0)
            {
                robot.SiteMap = new List<string>();

                foreach (var siteMap in matchSiteMap)
                    robot.Comments.Add(siteMap.ToString().TrimEnd('\r', '\n').ToLower().Replace("sitemap: ", string.Empty));
            }
        }

        private static void ParseCommentsData(MatchCollection matchComments, Robot robot)
        {
            if (matchComments != null && matchComments.Count > 0)
            {
                robot.Comments = new List<string>();

                foreach (var comments in matchComments)
                    robot.Comments.Add(comments.ToString().ToLower().TrimEnd('\r', '\n').Replace("# ", string.Empty));
            }
        }

        private static void ParseAllowData(MatchCollection matchAllow, Robot robot)
        {
            if (matchAllow != null && matchAllow.Count > 0)
            {
                robot.Allows = new List<string>();

                foreach (var allow in matchAllow)
                    robot.Disallows.Add(allow.ToString().ToLower().TrimEnd('\r', '\n').Replace("allow: ", string.Empty));
            }
        }

        private static void ParseDisallowData(MatchCollection matchDisallow, Robot robot)
        {
            if (matchDisallow != null && matchDisallow.Count > 0)
            {
                robot.Disallows = new List<string>();

                foreach (var disallow in matchDisallow)
                    robot.Disallows.Add(disallow.ToString().TrimEnd('\r', '\n').ToLower().Replace("disallow: ", string.Empty));
            }
        }

        private static void ParseUserAgentData(MatchCollection matchUserAgent, Robot robot)
        {
            if (matchUserAgent != null && matchUserAgent.Count > 0)
            {
                robot.UserAgent = new List<string>();

                foreach (var userAgent in matchUserAgent)
                    robot.UserAgent.Add(userAgent.ToString().TrimEnd('\r', '\n').ToLower().Replace("user-agent: ", string.Empty));
            }
        }

        private static MatchCollection GetRegexMatches(string textToTest, string pattern, RegexOptions options)
        {
            try
            {
                return Regex.Matches(textToTest, pattern, options);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when I try to use a Regex :(", ex);
            }
        }

        #endregion
    }
}