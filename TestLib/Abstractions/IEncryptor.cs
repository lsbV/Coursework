﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Abstractions
{
    public interface IEncryptor
    {
        string Encrypt(string data);
    }
}
