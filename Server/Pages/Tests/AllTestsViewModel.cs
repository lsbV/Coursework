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
using TestLib.Classes.Tasks;
using TestLib.Classes.Test;

namespace Server.Pages.Tests
{
    public partial class AllTestsViewModel : BaseViewModel, IUpdateable
    {
        #region Fields
        private IMessenger messenger;
        #endregion Fields


        #region ObservableProperties
        [ObservableProperty] ObservableCollection<Test> tests;
        #endregion ObservableProperties
        

        #region Constructors
        public AllTestsViewModel(IMessenger messenger)
        {
            Name = "Tests";
            this.messenger = messenger;
            Tests = new();           
        }
        #endregion Constructors


        #region Commands
        [RelayCommand]
        private void Edit(object param)
        {
            Test test = (Test)param;
            var testVM = new TestViewModel(test, messenger);
            WeakReferenceMessenger.Default.Send(testVM as BaseViewModel);
        }

        [RelayCommand]
        private async Task RemoveAsync(object param)
        {
            Test test = (Test)param;
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<Test>();
            await repo.RemoveAsync(test);
            await UpdateAsynk();
        }

        [RelayCommand]
        private void Add(object param)
        {
            var testVM = new TestViewModel(messenger);
            WeakReferenceMessenger.Default.Send(testVM as BaseViewModel);
        }

        [RelayCommand]
        private async Task RefreshAsync(object param)
        {
            await UpdateAsynk();
        }
        #endregion Commands


        #region Methods
        public async Task LoadTestsAsync()
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repoTest = uow.Repository<Test>();
            var list = await repoTest.GetAllAsync();
        }

        public async Task UpdateAsynk()
        {
            var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<Test>();
            Tests = new (await repo.GetAllAsync());
        }
        #endregion Methods
    }
}
