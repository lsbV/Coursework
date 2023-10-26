using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDesigner.ViewLib;
using TestDesigner;
using TestLib.Interfaces;

namespace TestDesigner
{
    public partial class ApplicationViewModel : BaseViewModel
    {
        private MainWindowViewModel main;
        private ApplicationController controller;
        [ObservableProperty] private BaseViewModel current;
        public ApplicationViewModel()
        {
            Name = "Application";
            main = new MainWindowViewModel();
            current = main;
            controller = new ApplicationController(this, main);
            main.Controller = controller;
        }
        public class ApplicationController
        {
            private ApplicationViewModel app;
            private MainWindowViewModel main;

            public ApplicationController(ApplicationViewModel app, MainWindowViewModel main)
            {
                this.app = app;
                this.main = main;
            }

            public void ChangeView(BaseViewModel view)
            {
                if (view == null)
                    throw new ArgumentNullException(nameof(view));
                app.Current = view;
            }
            public void AddTask(ITask task)
            {
                if (task == null)
                    throw new ArgumentNullException(nameof(task));
                main.Tasks.Add(task);
            }
            public void SetDefoultView()
            {
                app.Current = main;
            }
        }
    }
}
