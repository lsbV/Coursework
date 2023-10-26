using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Interfaces;

namespace TestLib.Classes.Bodies
{
    public class AudioTaskBody : ITaskBody
    {
        public string? Text { get; set; } = string.Empty;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
