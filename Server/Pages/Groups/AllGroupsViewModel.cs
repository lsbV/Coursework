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

namespace Server.Pages.Groups
{
    public partial class AllGroupsViewModel : BaseViewModel, IUpdateable, IRecipient<Group>
    {
        private IMessenger messenger;

        #region ObservableProperties
        [ObservableProperty] ObservableCollection<Group> groups = null!;
        #endregion ObservableProperties

        #region Properties
        #endregion Properties

        #region Constructors
        public AllGroupsViewModel(IMessenger messenger)
        {
            Name = "Groups";
            this.messenger = messenger;
            messenger.RegisterAll(this);
        }
        #endregion Constructors


        #region Commands
        [RelayCommand]
        private void Edit(object param)
        {
            Group group = (Group)param;
            var groupVM = new GroupViewModel(group, messenger);
            messenger.Send(groupVM as BaseViewModel);
        }

        [RelayCommand]
        private void Add(object param)
        {
            var groupVM = new GroupViewModel(messenger);
            messenger.Send(groupVM as BaseViewModel);
        }

        [RelayCommand]
        private async Task RemoveAsync(object param)
        {
            Group group = (Group)param;
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<Group>();
            await repo.RemoveAsync(group);
            await LoadGroupsAsync();
        }

        [RelayCommand]
        private void Cancel()
        {
            messenger.Send(new Group());
        }

        [RelayCommand]
        private async Task RefreshAsync()
        {
            await UpdateAsynk();
        }
        #endregion Commands


        #region Methods
        public async Task LoadGroupsAsync()
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repoGroup = uow.Repository<Group>();
            Groups = new(await repoGroup.GetAllAsync());
        }

        public async Task UpdateAsynk()
        {
            await LoadGroupsAsync();
        }

        public void Receive(Group message)
        {
            messenger.Send(this as BaseViewModel);
        }
        #endregion Methods
    }
}
