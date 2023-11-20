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
using TestLib;

namespace Server.Pages.Users
{
    public partial class AllUsersViewModel : BaseViewModel, IUpdateable, IRecipient<User>
    {
        #region Fields
        IMessenger messenger;
        #endregion Fields

        #region ObservableProperties
        [ObservableProperty] ObservableCollection<User> users;
        #endregion ObservableProperties

        #region Properties

        #endregion Properties

        #region Constructors
        public AllUsersViewModel(IMessenger messenger)
        {
            Name = "Users";
            Users = new ObservableCollection<User>();    
            this.messenger = messenger;
            messenger.RegisterAll(this);
        }
        #endregion Constructors

        #region Commands
        [RelayCommand]
        private void Edit(object param)
        {
            User user = (User)param;            
            var userVM = new UserViewModel(user, messenger);
            messenger.Send(userVM as BaseViewModel);
        }
        [RelayCommand]
        private async Task RemoveAsync(object param)
        {
            User user = (User)param;
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<User>();
            await repo.RemoveAsync(user); 
            await UpdateAsynk();
        }
        [RelayCommand]
        private void Add(object param)
        {
            var userVM = new UserViewModel(messenger);
            messenger.Send(userVM as BaseViewModel);
        }
        [RelayCommand]
        private async Task Refresh(object param)
        {
            await UpdateAsynk();
        }
        #endregion Commands

        #region Methods
        public async Task UpdateAsynk()
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<User>();
            Users = new(await repo.GetAllAsync());
        }

        public void Receive(User message)
        {
            messenger.Send(this as BaseViewModel);
        }
        #endregion Methods

    }
}
