﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DALTestsDB;
using Repository;
using Server.Ninject;
using Server.Pages.Groups;
using Server.Pages.Listener;
using Server.Pages.Tests;
using Server.Pages.TestsAssigned;
using Server.Pages.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestLib.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace Server.Pages.Application
{
    public partial class ApplicationViewModel : BaseViewModel, IRecipient<BaseViewModel>
    {
        IMessenger messenger;


        [ObservableProperty] ICollection<BaseViewModel> pages;
        [ObservableProperty] BaseViewModel current;
        [ObservableProperty] string status = string.Empty;
        [ObservableProperty] bool isBusy;
        public ApplicationViewModel()
        {            
            messenger = DI.Create<IMessenger>();
            messenger.RegisterAll(this);
            pages = new List<BaseViewModel>
            {
                new AllGroupsViewModel(messenger),
                new AllUsersViewModel(messenger),
                new AllTestsViewModel(messenger),
                new AllTestAssignedViewModel(messenger),
                new ListenerViewModel(DI.Create<IEncryptor>(), DI.Create<ISerializer>(), messenger)
            };
            Current = pages.First();
        }

        public void Receive(BaseViewModel message)
        {
            Current = message;
        }

        partial void OnCurrentChanged(BaseViewModel value)
        {
            if(RefreshCanExecute())
            {
                Task.Run(Refresh);
            }
        }

        [RelayCommand(CanExecute = nameof(RefreshCanExecute))]
        public async Task Refresh()
        {
            IsBusy = true;
            IUpdateable updateable = (IUpdateable)Current;
            await updateable.UpdateAsynk();
            IsBusy = false;
        }

        private bool RefreshCanExecute()
        {
            if (Current is IUpdateable)
            {
                return true;
            }
            return false;
        }
    }
}
