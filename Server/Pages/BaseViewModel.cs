using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Pages.Application
{
    public abstract class BaseViewModel : ObservableObject
    {
        public string Name { get; set; } = string.Empty;
    }
}
