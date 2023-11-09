using CommunityToolkit.Mvvm.ComponentModel;
using DALTestsDB;
using Repository;
using Server.Ninject;
using Server.Pages.Application;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Server.Pages.TestsAssigned
{
    public partial class TestAssignedViewModel : BaseViewModel, IUpdateable
    {
        private TestAssigned testAssigned;
        private ViewMode viewMode;

        #region ObservableProperties
        [ObservableProperty] int id;
        [ObservableProperty] string title = string.Empty;
        [ObservableProperty] string description = string.Empty;
        [ObservableProperty] DateTime startAt = DateTime.Now;
        [ObservableProperty] DateTime endAt = DateTime.Now;
        [ObservableProperty] TimeSpan activeTime = TimeSpan.Zero;
        [ObservableProperty] ProgresStatus status = ProgresStatus.NotStarted;
        [ObservableProperty] DateTime createdAt;
        [ObservableProperty] ObservableCollection<User> users;
        [ObservableProperty] ObservableCollection<User> allUsers;
        #endregion ObservableProperties

        #region Constructors
        public TestAssignedViewModel()
        {
            viewMode = ViewMode.Create;
            testAssigned = null!;
            Users = new();
            AllUsers = null!;
        }
        public TestAssignedViewModel(TestAssigned testAssigned) : this()
        {
            this.testAssigned = testAssigned;
            viewMode = ViewMode.Edit;
            InitFields(testAssigned);
        }
        #endregion Constructors

        private void InitFields(TestAssigned testAssigned)
        {
            this.testAssigned = testAssigned;
            this.Id = testAssigned.Id;  
            this.Title = testAssigned.Test.Title;
            this.Description = testAssigned.Test.Description;
            this.StartAt = testAssigned.StartAt;
            this.EndAt = testAssigned.EndAt;
            this.ActiveTime = testAssigned.ActiveTime;
            this.Status = testAssigned.Status;
            this.CreatedAt = testAssigned.CreatedAt;
        }

        public async Task UpdateAsynk()
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<TestAssigned>();
            var userRepo = uow.Repository<User>();
            await repo.LoadAssociatedPropertyAsync(testAssigned, x => x.Test);
            await repo.LoadAssociatedCollectionAsync(testAssigned, x => x.Users);
            Users = new (testAssigned.Users);
            AllUsers = new (await userRepo.GetAllAsync());
        }

        
    }
}