﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Classes.Network
{
    public class Message
    {
        public string Header { get; set; } = null!;
        public string? Body { get; set; } = null;
    }
}
