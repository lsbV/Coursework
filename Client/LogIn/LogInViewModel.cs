using Client.Infrastructure;
using Client.Server;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace Client.LogIn
{
    public partial class LogInViewModel : BaseViewModel
    {
        #region Fields
        private ApplicationController controller;
        IServerService serverService;
        #endregion Fields

        #region Observable Properties
        [ObservableProperty] string username;
        [ObservableProperty] int passwordLength;
        #endregion Observable Properties

        #region Constructors
        public LogInViewModel(ApplicationController controller, IServerService serverService)
        {
            this.controller = controller;
            this.serverService = serverService;

        }
        #endregion Constructors

        #region Commands
        [RelayCommand(IncludeCancelCommand = true)] 
        async Task LogIn(object param ,CancellationToken token)
        {
            var passwordBox = param as WatermarkPasswordBox;
            try
            {
                serverService.LogInAsync(username, passwordBox.Password);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("LogIn was canceled");
            }
        }
        #endregion Commands

        #region Methods
        public void LogIn()
        {

        }
        #endregion Methods
    }
}
