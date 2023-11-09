using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DALTestsDB;
using Microsoft.EntityFrameworkCore;
using Repository;
using Server.Ninject;
using Server.Pages.Application;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Interfaces;

namespace Server.Pages.Users
{
    public partial class AllUsersViewModel : BaseViewModel, IUpdateable, IRecipient<User>
    {
        #region Fields
        #endregion Fields

        #region ObservableProperties
        [ObservableProperty] ObservableCollection<User> users;
        #endregion ObservableProperties

        #region Properties

        #endregion Properties

        #region Constructors
        public AllUsersViewModel()
        {
            Name = "Users";
            Users = new ObservableCollection<User>();    
            WeakReferenceMessenger.Default.Register<User>(this);
        }
        #endregion Constructors

        #region Commands
        [RelayCommand]
        private void Edit(object param)
        {
            User user = (User)param;            
            var userVM = new UserViewModel(user);
            WeakReferenceMessenger.Default.Send(userVM as BaseViewModel);
        }
        [RelayCommand]
        private async Task RemoveAsync(object param)
        {
            User user = (User)param;
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<User>();
            await repo.RemoveAsync(user); 
            await LoadUsersAsynk();
        }
        [RelayCommand]
        private void Add(object param)
        {
            var userVM = new UserViewModel();
            WeakReferenceMessenger.Default.Send(userVM as BaseViewModel);
        }
        [RelayCommand]
        private async Task Refresh(object param)
        {
            await UpdateAsynk();
        }
        #endregion Commands

        #region Methods
        public async Task LoadUsersAsynk()
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<User>();
            Users = new (await repo.GetAllAsync());
        }  

        public async Task UpdateAsynk()
        {
            await LoadUsersAsynk();
        }

        public void Receive(User message)
        {
            WeakReferenceMessenger.Default.Send(this as BaseViewModel);
        }
        #endregion Methods

    }
}
