using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDesigner.ViewLib;
using TestDesigner;

namespace TestDesigner
{
    public partial class ApplicationViewModel : BaseViewModel
    {
        BaseViewModel main;
        [ObservableProperty] BaseViewModel current;
        ApplicationController controller;
        public ApplicationViewModel()
        {
            controller = new ApplicationController(this);
            Name = "Application";
            main = new MainWindowViewModel(controller);
            current = main;
        }
        public class ApplicationController
        {
            ApplicationViewModel model;

            public ApplicationController(ApplicationViewModel model)
            {
                this.model = model;
            }

            public void ChangeView(BaseViewModel view)
            {
                if (view == null)
                    throw new ArgumentNullException(nameof(view));
                model.Current = view;
            }
        }
    }
}
