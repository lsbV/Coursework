using Client.Infrastructure;
using Client.LogIn;
using Client.Main;
using Client.Server;
using Client.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Server.Ninject;
using System;
using System.Threading.Tasks;
using System.Timers;
using TestLib.Classes.Network;
using Timer = System.Timers.Timer;

namespace Client
{
    public partial class ApplicationViewModel : BaseViewModel,
        IRecipient<UserLogInedMessage>,
        IRecipient<ChangePageMessage>,
        IRecipient<ServerStartedSendingMessage>,
        IRecipient<ServerEndedSendingMessage>,
        IRecipient<ServerStartedRecivingMessage>,
        IRecipient<ServerEndedRecivingMessage>,
        IDisposable
    {
        IServerService server;
        IMessenger messenger;
        Timer timer;

        public BaseViewModel Main { get; private set;}
        public SettingsViewModel Settings { get; private set; }
        public static ApplicationViewModel Instance { get; } 


        [ObservableProperty] BaseViewModel current;
        [ObservableProperty] bool isConnection;
        [ObservableProperty] bool isBusy;
        [ObservableProperty] string status;
        
        protected ApplicationViewModel()
        {
            ViewName = "Application";
            server = DI.Create<IServerService>();
            messenger = DI.Create<IMessenger>();
            messenger.RegisterAll(this);
            Settings = SettingsViewModel.Instance;
            Current = new LogInViewModel(server, messenger);
            Main = null!;
            timer = new Timer(TimeSpan.FromSeconds(5));
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        static ApplicationViewModel()
        {
            Instance = new ApplicationViewModel();
        }

        [RelayCommand] 
        private void OpenSettings()
        {
            messenger.Send(new ChangePageMessage(Settings));
        }
        [RelayCommand]  
        private void Home()
        {
            messenger.Send(new ChangePageMessage(Main?? new LogInViewModel(server, messenger)));
        }


        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            IsConnection = server.IsConnected;
            Status = IsConnection ? "Connected" : "Disconnected";
        }
        public void Receive(UserLogInedMessage message)
        {
            Main = new MainViewModel(server, messenger);
            messenger.Send(new ChangePageMessage(Main));
        }
        public void Dispose()
        {
            server.SendMessageAsync(new Message() { Header = RequestMessage.LOG_OUT });
            server.Dispose();
            messenger.UnregisterAll(this);
            timer.Dispose();
            GC.SuppressFinalize(this);
        }

            

        void IRecipient<ServerEndedRecivingMessage>.Receive(ServerEndedRecivingMessage message)
        {
            IsBusy = false;
        }

        void IRecipient<ServerStartedRecivingMessage>.Receive(ServerStartedRecivingMessage message)
        {
            IsBusy = true;
        }

        void IRecipient<ServerEndedSendingMessage>.Receive(ServerEndedSendingMessage message)
        {
            IsBusy = false;
        }

        void IRecipient<ServerStartedSendingMessage>.Receive(ServerStartedSendingMessage message)
        {
            IsBusy = true;
        }

        void IRecipient<ChangePageMessage>.Receive(ChangePageMessage message)
        {
            if (Current is IDisposable disposable)
                disposable.Dispose();
            if (message.ViewModel is IUpdateable updateable)
                Task.WaitAll(updateable.UpdateAsynk());                
            Current = message.ViewModel;
        }
    }    
}
