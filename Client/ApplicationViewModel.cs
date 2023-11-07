using Client.Infrastructure;
using Client.Main;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Client
{
    public partial class ApplicationViewModel : BaseViewModel
    {
        BaseViewModel main;
        [ObservableProperty] BaseViewModel current;
        ApplicationController controller;
        public ApplicationViewModel()
        {
            controller = new ApplicationController(this);
            ViewName = "Application";
            main = new MainViewModel(controller);
            current = main;
        }
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
