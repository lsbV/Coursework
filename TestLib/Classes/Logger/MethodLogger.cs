using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;

namespace TestLib.Classes.Logger
{
    public class MethodLogger : ILogger
    {
        protected Action<string> log;
        public MethodLogger(Action<string> log)
        {
            this.log = log;
        }
        public void Log(string message, string err)
        {
            log(message);
        }
    }
}
