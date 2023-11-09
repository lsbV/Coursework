﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DALTestsDB;
using Repository;
using Server.Ninject;
using Server.Pages.Groups;
using Server.Pages.Tests;
using Server.Pages.TestsAssigned;
using Server.Pages.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Application
{
    public partial class ApplicationViewModel : BaseViewModel, IRecipient<BaseViewModel>
    {
        [ObservableProperty] ICollection<BaseViewModel> pages;
        [ObservableProperty] BaseViewModel current;
        [ObservableProperty] string status;
        [ObservableProperty] bool isBusy;
        public ApplicationViewModel()
        {            
            Name = "Application";
            pages = new List<BaseViewModel>();
            pages.Add(new AllUsersViewModel());
            pages.Add(new AllGroupsViewModel());
            pages.Add(new AllTestsViewModel());
            pages.Add(new AllTestAssignedViewModel());
            Current = pages.First();
            WeakReferenceMessenger.Default.Register<BaseViewModel>(this);
        }

        public void Receive(BaseViewModel message)
        {
            Current = message;
        }

        partial void OnCurrentChanged(BaseViewModel viewModel)
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
            if (Current is IUpdateable updateable)
            {
                return true;
            }
            return false;
        }
    }
}
