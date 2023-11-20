using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib;

namespace Client.Infrastructure
{
    public class FinishedPassingTestMessage
    {
        public required TestAssigned Test { get; set; }
    }
}
