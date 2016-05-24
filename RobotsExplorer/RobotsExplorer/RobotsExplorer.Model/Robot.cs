using System.Collections.Generic;

namespace RobotsExplorer.Model
{
    public class Robot
    {
        public string Domain { get; set; }
        public List<string> Allows { get; set; }
        public List<string> Disallows { get; set; }
        public List<string> Comments { get; set; }
        public List<string> UserAgent { get; set; }
        public List<string> SiteMap { get; set; }
    }
}