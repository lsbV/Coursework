using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDesigner.ViewLib;
using TestLib.Abstractions;

namespace TestDesigner.Body
{
    public abstract class BaseBodyViewModel : BaseViewModel
    {
        public abstract TaskBody CreateBody();
        public BaseBodyViewModel() { }
    }
}
