using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Infrastructure
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]private string viewName = string.Empty;
    }
}
