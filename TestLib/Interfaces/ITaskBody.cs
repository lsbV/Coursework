﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Interfaces
{
    public interface ITaskBody : ICloneable
    {
        public string? Text { get; set; }
    }
}
