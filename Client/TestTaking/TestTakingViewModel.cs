using Client.Infrastructure;
using Client.MVVM_Task._Task;
using Client.Server;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Documents;
using TestLib;
using TestLib.Abstractions;
using TestLib.Classes.Network;
using TestLib.Classes.Test;
using Xceed.Wpf.Toolkit;

namespace Client.TestTaking
{
    public partial class TestTakingViewModel : BaseViewModel, IDisposable
    {
        #region Fields
        private TestAssigned test;
        private System.Timers.Timer timer;
        private TimeSpan timeOutForRequest;
        private IMessenger messenger;
        private IServerService server;
        private ISerializer serializer;
        #endregion Fields

        #region Observable Properties
        [ObservableProperty] TimeSpan timeLeft;
        [ObservableProperty] List<BaseTaskVM> tasks;
        #endregion Observable Properties

        #region Constructors
        public TestTakingViewModel(TestAssigned test, IServerService server, ISerializer serializer, IMessenger messenger)
        {
            timeOutForRequest = TimeSpan.FromSeconds(5);
            this.test = test;
            TimeLeft = test.TimeToTake;
            timer = new(1000);
            timer.Elapsed += TimerTik;
            Tasks = test.Test.Tasks.Select(t=>t.GetTaskViewModel()).ToList();
            timer.Start();
            this.server = server;
            this.serializer = serializer;
            this.messenger = messenger;
        }

        #endregion Constructors

        #region Commands
        [RelayCommand(IncludeCancelCommand = true)]
        private async System.Threading.Tasks.Task EndTestAsync(CancellationToken token)
        {
            timer.Stop();
            test.Test.Tasks = Tasks.Select(t=>t.GetTaskResult()).ToList();

            try
            {
                await server.SendMessageAsync(new Message() { Header = RequestMessage.PUT, Body = serializer.Serialize(test) });
                //using CancellationTokenSource source = new(timeOutForRequest);
                Message? message = await server.ReciveMessageAsync(token);
                if (message != null && message.Header == ResponseCode.OK)
                {
                    System.Windows.MessageBox.Show($"Test result: {message.Body}");
                }                
                else
                    System.Windows.MessageBox.Show("Server send bad answer!");
                timer.Dispose();
                messenger.Send(new FinishedPassingTestMessage() { Test = test });
            }
            catch (OperationCanceledException)
            {
                System.Windows.MessageBox.Show("Server not responding!");
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Something went wrong");
            }
        }
        #endregion Commands

        #region Methods
        private void TimerTik(object? sender, ElapsedEventArgs e)
        {
            TimeLeft = TimeLeft.Subtract(TimeSpan.FromSeconds(1));
            if (TimeLeft == TimeSpan.Zero)
            {
                timer.Stop();
                EndTestCommand.Execute(null);
            }
        }
        public void Dispose()
        {
            timer.Dispose();
        }
        #endregion Methods
    }
}
