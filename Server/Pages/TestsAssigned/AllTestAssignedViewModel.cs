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

namespace Server.Pages.TestsAssigned
{
    public partial class AllTestAssignedViewModel : BaseViewModel, IUpdateable, IRecipient<TestAssigned>
    {
        private readonly IMessenger messenger;

        #region ObservableProperties
        [ObservableProperty] ObservableCollection<TestAssigned> testsAssigned;
        #endregion ObservableProperties

        #region Constructors
        public AllTestAssignedViewModel(IMessenger messenger)
        {
            Name = "Tests assigned";
            TestsAssigned = new();
            this.messenger = messenger;
            messenger.RegisterAll(this);
        }
        #endregion Constructors

        #region Commands
        [RelayCommand]
        private void Edit(object param)
        {
            var testAssigned = (TestAssigned)param;
            var testAssignedVM = new TestAssignedViewModel(testAssigned, messenger);
            messenger.Send(testAssignedVM as BaseViewModel);
        }
        [RelayCommand]
        private async Task RemoveAsync(object param)
        {
            var testAssigned = (TestAssigned)param;
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<TestAssigned>();
            await repo.RemoveAsync(testAssigned);
            await UpdateAsynk();
        }
        [RelayCommand]
        private void Add(object param)
        {
            var testAssignedVM = new TestAssignedViewModel(messenger);
            messenger.Send(testAssignedVM as BaseViewModel);
        }
        [RelayCommand]
        private async Task RefreshAsync(object param)
        {
            await UpdateAsynk();
        }
        #endregion Commands

        #region Methods
        public async Task UpdateAsynk()
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<TestAssigned>();
            var tests = await repo.GetAllAsync();
            foreach(var test in tests)
            {
                await repo.LoadAssociatedPropertyAsync(test, t=>t.Test);
            }
            TestsAssigned = new(tests);
        }

        public void Receive(TestAssigned message)
        {
            messenger.Send(this as BaseViewModel);
        }
        #endregion Methods
    }
}
