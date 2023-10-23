using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDesigner.ViewLib
{
    public abstract class BaseViewModel : ObservableObject
    {
        public string Name { get; set; }
    }
}
