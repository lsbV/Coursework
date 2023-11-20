using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Abstractions
{
    public interface IMessageBox
    {
        void Show(string message);
        void Show(string message, string caption);
    }
}
