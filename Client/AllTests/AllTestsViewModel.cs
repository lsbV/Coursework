using Client.Infrastructure;
using Client.Server;
using Client.TestTaking;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Server.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestLib;
using TestLib.Abstractions;
using TestLib.Classes.Network;
using TestLib.Classes.Test;
using Xceed.Wpf.Toolkit;
using Task = System.Threading.Tasks.Task;

namespace Client.AllTests
{
    public partial class AllTestsViewModel : BaseViewModel, IDisposable, IUpdateable
    {
        #region Fields
        private IServerService server;
        private ISerializer serializer;
        private IMessenger messenger;
        private TimeSpan timeOutForRequest;
        #endregion Fields

        #region Observable Properties
        [ObservableProperty] List<TestAssigned> tests;
        #endregion Observable Properties

        #region Constructors
        public AllTestsViewModel(IServerService server, IMessenger messenger)
        {
            this.server = server;
            this.serializer = DI.Create<ISerializer>();
            this.messenger = messenger;
            timeOutForRequest = TimeSpan.FromSeconds(10);
            Tests = new();
        }
        #endregion Constructors

        #region Commands
        [RelayCommand(IncludeCancelCommand = true)]
        private async Task LoadTests(CancellationToken token)
        {
            try
            {
                Tests.Clear();
                await server.SendMessageAsync(new Message() { Header = RequestMessage.GET, Body = RequestMessage.LIST_TESTS });
                var msg = await server.ReciveMessageAsync(token);

                if (msg == null)
                    throw new Exception("Server send bad answer!");
                if (msg.Header == ResponseCode.ERROR)
                    throw new Exception(msg.Body);
                if (msg.Header == ResponseCode.NOT_FOUND)
                    throw new Exception("You have no active tests!");

                var tests = serializer.Deserialize<List<TestAssigned>?>(msg.Body);
                if (tests != null)
                    Tests = tests;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        [RelayCommand(IncludeCancelCommand = true)]
        private async Task StartTestAsync(object param, CancellationToken token)
        {
            TestAssigned test = (TestAssigned)param;
            try
            {
                await server.SendMessageAsync(new Message() { Header = RequestMessage.START_TEST, Body = test.Id.ToString() });
                var msg = await server.ReciveMessageAsync(token);

                if (msg == null)
                    throw new Exception("Server send bad answer!");
                if (msg.Header == ResponseCode.ERROR)
                    throw new Exception(msg.Body);
                if (msg.Header == ResponseCode.NOT_FOUND)
                    throw new Exception("Test not found!");
                var fullTest = serializer.Deserialize<TestAssigned>(msg.Body);
                if (fullTest == null)
                    throw new Exception("Server send bad answer!");

                var testViewModel = new TestTakingViewModel(fullTest, server, serializer, messenger);
                messenger.Send(new ChangePageMessage(testViewModel));
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Server not responding!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion Commands

        #region Methods
        public void Dispose()
        {
            LoadTestsCancelCommand?.Execute(null);
        }

        public async Task UpdateAsynk()
        {
            try
            {
                using CancellationTokenSource source = new CancellationTokenSource(timeOutForRequest);
                await LoadTests(source.Token);
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Server not responding!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion Methods
    }
}
