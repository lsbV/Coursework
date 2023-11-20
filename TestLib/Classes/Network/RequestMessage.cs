using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Classes.Network
{
    public static class RequestMessage
    {
        public const string LOG_IN = "LOG_IN";
        public const string LOG_OUT = "LOG_OUT";
        public const string GET = "GET";
        public const string PUT = "PUT";
        public const string LIST_TESTS = "LIST_TESTS";
        public const string START_TEST = "START_TEST";
    }
}
