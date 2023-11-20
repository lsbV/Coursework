using Client.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;

namespace Client.MVVM_Task._Body
{
    public abstract partial class BaseBodyVM : BaseViewModel
    {
        protected Body body;

        [ObservableProperty] string text;

        public BaseBodyVM(Body body)
        {
            this.body = body;
            Text = body.Text;
        }
    }
}
