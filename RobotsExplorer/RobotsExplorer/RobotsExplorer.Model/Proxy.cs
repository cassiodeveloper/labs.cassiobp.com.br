using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsExplorer.Model
{
    public class Proxy
    {
        public string Protocol { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public int Port { get; set; }
    }
}