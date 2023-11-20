using Client.Infrastructure;
using Client.Server;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TestLib.Classes.Network;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace Client.LogIn
{
    public partial class LogInViewModel : BaseViewModel
    {
        #region Fields
        private IServerService serverService;
        private IMessenger messenger;
        #endregion Fields

        #region Observable Properties
        [ObservableProperty] string username = string.Empty;
        #endregion Observable Properties

        #region Constructors
        public LogInViewModel(IServerService serverService, IMessenger messenger)
        {
            ViewName = "LogIn";
            this.serverService = serverService;
            this.messenger = messenger;
            Username = "user";
        }
        #endregion Constructors

        #region Commands
        [RelayCommand(IncludeCancelCommand = true)] 
        async Task LogIn(object param ,CancellationToken token)
        {
            var passwordBox = (WatermarkPasswordBox)param;
            try
            {
                if(serverService.IsConnected == false)
                    await serverService.ConnectAsync(Settings.Settings.ServerIPEndPoint, token);

                await serverService.LogInAsync(Username, passwordBox.Password);

                var msg = await serverService.ReciveMessageAsync(token);
                if (msg != null)
                {
                    if (msg.Header == ResponseCode.OK)
                    {
                        messenger.Send(new UserLogInedMessage());
                    }
                    else
                    {
                        MessageBox.Show($"LogIn failed\n{msg.Body}");
                    }
                }
                
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("LogIn was canceled");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion Commands

        #region Methods
        private bool CanLogIn(object param)
        {
            return !string.IsNullOrWhiteSpace(Username);
        }
        #endregion Methods
    }
}
