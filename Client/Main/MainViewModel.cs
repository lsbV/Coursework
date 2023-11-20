using Client.AllTests;
using Client.Infrastructure;
using Client.Server;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Main
{
    public partial class MainViewModel : BaseViewModel, IRecipient<FinishedPassingTestMessage>
    {
        #region Fields
        IMessenger messenger;
        #endregion Fields


        #region ObservableProperties
        [ObservableProperty] BaseViewModel current;
        [ObservableProperty] List<BaseViewModel> items;
        #endregion ObservableProperties


        #region Constructor
        public MainViewModel(IServerService server, IMessenger messenger)
        {
            this.messenger = messenger;
            messenger.RegisterAll(this);
            ViewName = "Main";
            Items = new List<BaseViewModel>()
            {
                new AllTestsViewModel(server, messenger),
            };
            //Items.Add(new ServerViewModel(server, messenger));
            //Items.Add(new LogOutViewModel(server, messenger));
            Current = Items.First();
        }
        #endregion Constructor


        #region Methods

        public void Receive(FinishedPassingTestMessage message)
        {
            Current = Items.First();
            Task.Run( async () => await UpdateAsynk());
            messenger.Send(new ChangePageMessage(this));
        }

        public async Task UpdateAsynk()
        {
            if (Current is IUpdateable updateable)
            {
                await updateable.UpdateAsynk();
            }
            return;
        }

        #endregion Methods

    }
}