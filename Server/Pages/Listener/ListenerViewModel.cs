using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DALTestsDB;
using Newtonsoft.Json;
using Repository;
using Server.Ninject;
using Server.Pages.Application;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Threading;
using TestLib.Classes.Network;
using TestLib.Classes.Test;
using Xceed.Wpf.Toolkit;

namespace Server.Pages.Listener
{
    public partial class ListenerViewModel : BaseViewModel, IUpdateable
    {
        #region Fields
        private TcpListener listener = null!;
        #endregion Fields
        #region ObservableProperties
        [ObservableProperty] int port;
        //[ObservableProperty] string ip;
        [ObservableProperty] IPAddress[] iPAddresses = null!;
        [ObservableProperty] IPAddress selectedIPAddress = null!;
        [ObservableProperty] ObservableCollection<User> users = null!;
        #endregion ObservableProperties
        #region Properties
        #endregion Properties
        #region Constructors
        public ListenerViewModel()
        {
            Name = "Listener";
            //NetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            //selectedNetworkInterface = NetworkInterfaces.FirstOrDefault();

        }

        public async Task UpdateAsynk()
        {
            var host = await Dns.GetHostEntryAsync(Dns.GetHostName());
            IPAddresses = host.AddressList;

        }
        #endregion Constructors
        #region Commands
        [RelayCommand]
        private void Start()
        {
            if (Validate(out string msg) == false)
            {
                MessageBox.Show(msg);
            }
            listener = new TcpListener(SelectedIPAddress, Port);
            listener.Start();
            //var client = await listener.AcceptTcpClientAsync();
            //var stream = client.GetStream();
            //var buffer = new byte[1024];
            //var read = await stream.ReadAsync(buffer, 0, buffer.Length);
            //var message = Encoding.UTF8.GetString(buffer, 0, read);
            
        }

        [RelayCommand]
        private void Stop()
        {
            listener.Stop();
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
            //if (string.IsNullOrWhiteSpace(Ip))
            //{
            //    msg = "IP is required";
            //    return false;
            //}
            //if(IPAddress.TryParse(Ip, out var ip) == false)
            //{
            //    msg = "IP is not valid";
            //    if()
            //    return false;
            //}
            if (SelectedIPAddress == null)
            {
                msg = "IP is required";
                return false;
            }

            return true;

        }
        public void Listen()
        {
            while(true)
            {
                var client = listener.AcceptTcpClient();
                var stream = client.GetStream();
                var buffer = new byte[1024];
                var read = stream.Read(buffer, 0, buffer.Length);
                var message = Encoding.UTF8.GetString(buffer, 0, read);
                Console.WriteLine(message);
            }
        }
        public async void ListenClient(TcpClient client)
        {
            var stream = client.GetStream();
            var writer = new StreamWriter(stream);
            var reader = new StreamReader(stream);
            User user = null!;
            var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };
            bool work = true;
            while (work)
            {
                StringBuilder builder = new StringBuilder();
                while(stream.DataAvailable)
                {
                    builder.AppendLine(await reader.ReadLineAsync());
                }
                var message = JsonConvert.DeserializeObject<Message>(builder.ToString());
                if(message.Header == RequestMethod.CONNECT)
                {
                    var LP = message.Body.Split(" ");
                    var login = LP[0];
                    var password = LP[1];
                    var uow = DI.Create<IGenericUnitOfWork>();
                    user = uow.Repository<User>().FindAllAsync(u => u.Login == login && u.Password == password).Result.Single();
                    string answer = string.Empty;
                    if (Users.Any(u => u.Id == user.Id))
                    {                        
                        answer = JsonConvert.SerializeObject(new Message() { Header = ResponseCode.ERROR, Body = "The user is already logged in" });
                    }
                    else
                    {
                        Dispatcher.CurrentDispatcher.Invoke(() => Users.Add(user));
                        answer = JsonConvert.SerializeObject(new Message() { Header = ResponseCode.OK, Body = $"Hello {user.FirstName}" });
                    }                    
                    await writer.WriteAsync(answer);
                    continue;
                }
                if(message.Header == RequestMethod.DISCONNECT)
                {
                    Dispatcher.CurrentDispatcher.Invoke(() => Users.Remove(user));
                    work = false;
                    break;
                }
                if(message.Header == RequestMethod.GET)
                {
                    var uow = DI.Create<IGenericUnitOfWork>();
                    await uow.Repository<User>().LoadAssociatedCollectionAsync(user, u => u.TestAssigneds);
                    var tests= user.TestAssigneds.Select(ta => ta.Test.GetClearTest()).ToList();
                    string answer = null!;
                    if (tests.Count == 0)
                    {
                        answer = JsonConvert.SerializeObject(new Message() { Header = ResponseCode.NOT_FOUND, Body = "No active tests" });
                    }
                    else
                    {
                        var testsJson = JsonConvert.SerializeObject(tests, jsonSettings);
                        answer = JsonConvert.SerializeObject(new Message() { Header = ResponseCode.OK, Body = testsJson }, jsonSettings);
                    }                    
                    await writer.WriteAsync(answer);
                    continue;
                }
                if(message.Header == RequestMethod.POST)
                {
                    var test = JsonConvert.DeserializeObject<Test>(message.Body, jsonSettings);
                    var uow = DI.Create<IGenericUnitOfWork>();
                    UserTestResult userTestResult = new UserTestResult() { PassageDate = DateTime.Now, UserTaskResults = test.Tasks };
                    continue;
                }
                
            }
            client.Close();
        }

        #endregion Methods

    }
}
