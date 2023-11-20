using Client.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Server.Ninject;
using System.Net;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace Client.Settings
{
    public partial class SettingsViewModel : BaseViewModel, IUpdateable
    {
        #region Fields
        IMessenger messenger;
        #endregion Fields


        #region Properties
        public static SettingsViewModel Instance { get; }
        #endregion Properties


        #region Observable Properties
        [ObservableProperty] string serverIP;
        [ObservableProperty] int serverPort;
        #endregion Observable Properties


        protected SettingsViewModel()
        {
            ViewName = "Settings";
            this.messenger = DI.Create<IMessenger>();
        }
        static SettingsViewModel()
        {
            Instance = new SettingsViewModel();
        }


        [RelayCommand]
        public void Save()
        {
            if (ServerPort < 1 && ServerPort > 65535)
            {
                MessageBox.Show("ServerPort must be between 1 and 65535", "Error");
            }
            else if (IPAddress.TryParse(ServerIP, out IPAddress? address) == false)
            {
                MessageBox.Show(ServerIP + " is not a valid IP address", "Error");
            }
            var newIp = new IPEndPoint(IPAddress.Parse(ServerIP), ServerPort);
            Settings.ServerIPEndPoint = newIp;
            messenger.Send(new SettingsWereChangedMessage());
        }
            
        [RelayCommand]
        public void Cancel()
        {
            messenger.Send(new SettingsWereChangedMessage());
        }

        public Task UpdateAsynk()
        {
            ServerIP = Settings.ServerIPEndPoint.Address.ToString();
            ServerPort = Settings.ServerIPEndPoint.Port;
            return Task.CompletedTask;
        }
    }
    
}