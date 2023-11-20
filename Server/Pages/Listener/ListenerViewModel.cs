using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DALTestsDB;
using Newtonsoft.Json;
using Repository;
using Server.Infrastructure;
using Server.Ninject;
using Server.Pages.Application;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using TestLib;
using TestLib.Abstractions;
using TestLib.Classes.Logger;
using TestLib.Classes.Network;
using TestLib.Classes.Test;
using Xceed.Wpf.Toolkit;
using Task = System.Threading.Tasks.Task;

namespace Server.Pages.Listener
{
    public partial class ListenerViewModel : BaseViewModel, IDisposable, IRecipient<ClientConnectedMessage>, IRecipient<ClientDisconnectedMessage>
    {
        #region Fields
        private TcpListener? listener;
        private IEncryptor encryptor;
        private ISerializer serializer;
        private ILogger logger;
        private IMessenger messenger;
        #endregion Fields


        #region ObservableProperties
        [ObservableProperty] int port;
        [ObservableProperty] string log = string.Empty;
        [ObservableProperty] IPAddress[] iPAddresses;
        [ObservableProperty] IPAddress selectedIPAddress;
        [ObservableProperty] ObservableCollection<ServerUser> users;
        #endregion ObservableProperties


        #region Constructors
        public ListenerViewModel(IEncryptor encryptor, ISerializer serializer, IMessenger messenger)
        {
            Name = "Listener";
            logger = new MethodLogger((log) => Log += DateTime.Now.ToString("HH:mm:ss.fff") + " " + log + Environment.NewLine );
            Port = 50001;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddresses = host.AddressList;
            SelectedIPAddress = IPAddresses.Last();
            Users = new ObservableCollection<ServerUser>();
            System.Threading.Tasks.Task.Run(Start);
            this.encryptor = encryptor;
            this.serializer = serializer;
            this.messenger = messenger;
            messenger.RegisterAll(this);
        }
        #endregion Constructors


        #region Commands
        [RelayCommand]
        private void Start()
        {
            if(listener != null)
            {
                logger.Log("The listener is already running");
                return;
            }
            if (Validate(out string msg) == false)
            {
                MessageBox.Show(msg);
            }
            listener = new TcpListener(SelectedIPAddress, Port);
            listener.Start();
            logger.Log($"The listener started on {SelectedIPAddress}:{Port}");
            System.Threading.Tasks.Task.Run(Listen);
        }

        [RelayCommand]
        private void Stop()
        {
            if (listener == null)
            {
                logger.Log("The listener is not running");
                return;
            }
            listener.Stop();
            listener = null;
        }
        [RelayCommand]
        private void Kick(object param)
        {
            ServerUser serverUser = (ServerUser)param;
            if(UserPool.Instance.Contains(serverUser.WorkerId))
            {
                serverUser.CancellationSource.Cancel();
            }
            if(Users.Contains(serverUser))
            {
                Users.Remove(serverUser);
            }
        }
        #endregion Commands


        #region Methods
        private bool Validate(out string msg)
        {
            msg = string.Empty;
            if (Port == 0)
            {
                msg = "Port is required";
                return false;
            }
            if (SelectedIPAddress == null)
            {
                msg = "IP is required";
                return false;
            }

            return true;

        }
        public void Listen()
        {
            try
            {
                while (true)
                {
                    var client = listener!.AcceptTcpClient();
                    Task.Run(async () => await ListenClient(client));
                    //Thread.Sleep(100);
                }
            }   
            catch (SocketException)
            {
                logger.Log("The listener stopped");
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
        }
        public async Task ListenClient(TcpClient client)
        {
            using ServerWorker worker = new ServerWorker(client, serializer, logger, encryptor);
            using CancellationTokenSource source = new CancellationTokenSource();
            ServerUser serverUser = new ServerUser(worker, source);
            try
            {
                UserPool.Instance.Add(worker.Id, serverUser);
                await worker.Work(source.Token);
            }
            catch (OperationCanceledException)
            {
                logger.Log($"Client {worker.IP} forcibly disconnected");
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
            finally
            {
                if(UserPool.Instance.Contains(worker.Id))
                {
                    UserPool.Instance.Remove(worker.Id);
                }
            }
        }

        public void Dispose()
        {
            if (listener != null)
            {
                listener.Stop();
            }
            UserPool.Instance.Dispose();
        }

        void IRecipient<ClientConnectedMessage>.Receive(ClientConnectedMessage message)
        {
            App.Current.Dispatcher.BeginInvoke((Action)delegate ()
            {
                Users.Add(message.Client);
            });


        }

        void IRecipient<ClientDisconnectedMessage>.Receive(ClientDisconnectedMessage message)
        {
            var user = Users.FirstOrDefault(x => x.WorkerId == message.WorkerId);
            if(user != null)
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    Users.Remove(user);
                });
            }
        }
        #endregion Methods
    }
}
