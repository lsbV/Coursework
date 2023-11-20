using Server.Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var applicationViewModel = DI.Create<ApplicationViewModel>();
            var applicationView = new ApplicationView();
            applicationView.DataContext = applicationViewModel;
            applicationView.Show();
            base.OnStartup(e);
        }
        override protected void OnExit(ExitEventArgs e)
        {
            DI.Create<ApplicationViewModel>().Dispose();
            base.OnExit(e);
        }
    }
}
