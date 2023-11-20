using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DALTestsDB;
using Repository;
using Server.Infrastructure;
using Server.Ninject;
using Server.Pages.Application;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestLib;
using TestLib.Classes.Enums;
using TestLib.Classes.Test;

namespace Server.Pages.TestsAssigned
{
    public partial class TestAssignedViewModel : BaseViewModel, IUpdateable
    {
        #region Fields
        private readonly ViewMode viewMode;
        private readonly IMessenger messenger;
        #endregion Fields

        #region ObservableProperties
        [ObservableProperty] int id;
        [ObservableProperty] string? title = string.Empty;
        [ObservableProperty] string? description = string.Empty;
        [ObservableProperty] DateTime startAt = DateTime.Now;
        [ObservableProperty] TimeSpan activeTime = TimeSpan.Zero;
        [ObservableProperty] TimeSpan timeToTake = TimeSpan.Zero;
        [ObservableProperty] DateTime createdAt = DateTime.Now;
        [ObservableProperty] DateTime endAt = DateTime.Now;
        [ObservableProperty] bool isArchived;
        [ObservableProperty] Test test;
        [ObservableProperty] ObservableCollection<User> users;
        [ObservableProperty] ObservableCollection<User> allUsers;
        [ObservableProperty] ObservableCollection<Test> allTests;
        #endregion ObservableProperties


        #region Properties
        public ProgresStatus Status => StartAt.GetStatus(EndAt);
        #endregion Properties


        #region Constructors
        public TestAssignedViewModel(IMessenger messenger)
        {
            viewMode = ViewMode.Create;
            Users = new();
            AllUsers = null!;
            AllTests = null!;
            test = null!;
            this.messenger = messenger;
        }
        public TestAssignedViewModel(TestAssigned testAssigned, IMessenger messenger) : this(messenger)
        {
            viewMode = ViewMode.Edit;
            InitFields(testAssigned);
        }
        #endregion Constructors


        #region Commands
        [RelayCommand]
        public async Task SaveAsync()
        {
            if (!Validate(out string errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }
            using var uow = DI.Create<IGenericUnitOfWork>();
            var test = await CreateAssignedTest(uow);
            var repo = uow.Repository<TestAssigned>();
            if (viewMode == ViewMode.Create)
            {
                await repo.AddAsync(test);
            }
            else
            {                
                await repo.UpdateAsync(test);
            }
            WeakReferenceMessenger.Default.Send(test);
        }

        [RelayCommand]
        private void Add(object param)
        {
            var user = (User)param;
            Users.Add(user);
        }

        [RelayCommand]
        private void Remove(object param)
        {
            var user = (User)param;
            Users.Remove(user);
        }

        [RelayCommand]
        private void Cancel()
        {
            WeakReferenceMessenger.Default.Send(new TestAssigned());
        }

        [RelayCommand]
        private void ChooseTest(object param)
        {
            Test = (Test)param;
            Title = Test.Title;
            Description = Test.Description;
        }
        #endregion Commands


        #region Methods
        private bool Validate(out string errorMessage)
        {
            if(Test == null || Test.Id == 0)
            {
                errorMessage = "Test is not selected";
                return false;
            }
            if(StartAt < DateTime.Now)
            {
                errorMessage = "Start time is in the past";
                return false;
            }
            if(ActiveTime == TimeSpan.Zero)
            {
                errorMessage = "Active time is zero";
                return false;
            }
            if(Users.Count == 0)
            {
                errorMessage = "Users are not selected";
                return false;
            }   
            errorMessage = string.Empty;
            return true;
        }

        private async Task<TestAssigned> CreateAssignedTest(IGenericUnitOfWork uow)
        {
            var testAssignedRepo = uow.Repository<TestAssigned>();
            TestAssigned testAssigned = await GetTestAssigned(testAssignedRepo);
            testAssigned.TestId = Test.Id;
            testAssigned.StartAt = StartAt;
            testAssigned.EndAt = EndAt;
            testAssigned.TimeToTake = TimeToTake;
            testAssigned.CreatedAt = CreatedAt;
            testAssigned.IsArchived = IsArchived;
            await PushUsersToTest(uow, testAssignedRepo, testAssigned);
            return testAssigned;
        }

        private async Task PushUsersToTest(IGenericUnitOfWork uow, IGenericRepository<TestAssigned> testAssignedRepo, TestAssigned testAssigned)
        {
            var usersId = Users.Select(x => x.Id).ToArray();
            var newUsers = await uow.Repository<User>().FindAllAsync(x => usersId.Contains(x.Id));
            if (viewMode == ViewMode.Create)
            {
                testAssigned.Users = newUsers.ToList();
            }
            else if (viewMode == ViewMode.Edit)
            {
                await testAssignedRepo.LoadAssociatedCollectionAsync(testAssigned, x => x.Users);
                testAssigned.Users.Clear();
                testAssigned.Users.AddRange(newUsers);
            }
        }

        private async Task<TestAssigned> GetTestAssigned(IGenericRepository<TestAssigned> testAssignedRepo)
        {
            TestAssigned? testAssigned;
            if (viewMode == ViewMode.Edit)
            {
                testAssigned = await testAssignedRepo.FindByIdAsync(this.Id);
                if (testAssigned == null) 
                    throw new ArgumentException("Assigned test already deleted");
            }
            else
                testAssigned = new TestAssigned();
            return testAssigned;
        }

        private void InitFields(TestAssigned testAssigned)
        {
            this.Id = testAssigned.Id;
            this.Test = testAssigned.Test;
            this.Title = testAssigned.Test?.Title;
            this.Description = testAssigned.Test?.Description;
            this.StartAt = testAssigned.StartAt;
            this.TimeToTake = testAssigned.TimeToTake;
            this.CreatedAt = testAssigned.CreatedAt;
            Task.Run(() => {
                using var uow = DI.Create<IGenericUnitOfWork>();
                var repo = uow.Repository<TestAssigned>();
                repo.LoadAssociatedCollection(testAssigned, x => x.Users);
                Users = new(testAssigned.Users);
            });
        }

        partial void OnStartAtChanged(DateTime value)
        {
            OnPropertyChanged(nameof(Status));
        }

        partial void OnEndAtChanged(DateTime value)
        {
            OnPropertyChanged(nameof(Status));
        }

        public async Task UpdateAsynk()
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var userRepo = uow.Repository<User>();
            AllUsers = new(await userRepo.GetAllAsync());
            var testRepo = uow.Repository<Test>();
            AllTests = new(await testRepo.GetAllAsync());
        }
        #endregion Methods
    }
}