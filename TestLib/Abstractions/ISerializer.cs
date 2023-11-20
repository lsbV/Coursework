using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Abstractions
{
    public interface ISerializer
    {
        string Serialize(object obj);
        T? Deserialize<T>(string obj);
    }
}
