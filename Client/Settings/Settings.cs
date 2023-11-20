using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.Settings
{
    public static class Settings
    {
        public static IPEndPoint ServerIPEndPoint { get; set; } = new IPEndPoint(IPAddress.Parse("192.168.0.106"), 50001);
        public static string FtpHostAddress { get; set; } = "localhost";
    }
}
