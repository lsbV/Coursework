using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DALTestsDB;
using Repository;
using Server.Ninject;
using Server.Pages.Application;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Server.Pages.Groups
{
    public partial class GroupViewModel : BaseViewModel, IUpdateable
    {
        #region Fields
        private Group group;
        private ViewMode mode;
        #endregion Fields
        #region ObservableProperties
        [ObservableProperty] string name = string.Empty;
        [ObservableProperty] string? description = string.Empty;
        [ObservableProperty] bool isArchived = false;
        [ObservableProperty] DateTime createdAt = DateTime.Now;
        [ObservableProperty] ObservableCollection<User> users;
        [ObservableProperty] ObservableCollection<User> allUsers;
        #endregion ObservableProperties
        #region Properties
        #endregion Properties
        #region Constructors
        public GroupViewModel()
        {
            group = null!;
            Name = "User";
            mode = ViewMode.Create;
            //AllUsers = new ();
            Users = new ();

        }
        public GroupViewModel(Group group) : this()
        {
            Name = "Group";
            InitFields(group);
        }

        
        #endregion Constructors

        #region Commands
        [RelayCommand]
        public async Task Save()
        {
            if (ValidateData(out string msg) == false)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show(msg);
                return;
            }
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repoGroup = uow.Repository<Group>();
            var repoUser = uow.Repository<User>();
            Group group = CreateGroup();
            if (mode == ViewMode.Create)
            {
                await repoGroup.AddAsync(group);
            }
            else
            {
                await repoGroup.UpdateAsync(group);
            }
            WeakReferenceMessenger.Default.Send(group);
        }
        [RelayCommand]
        private void AddUser(object param)
        {
            User user = (User)param;
            if(Users.Contains(user))
            {
                return;
            }
            Users.Add(user);
        }
        [RelayCommand] 
        private void RemoveUser(object param)
        {
            User user = (User)param;
            Users.Remove(user);
        }
        #endregion Commands

        #region Methods
        private Group CreateGroup()
        {
            Group group = this.group ?? new();
            group.Name = Name;
            group.Description = Description;
            group.IsArchived = IsArchived;
            group.CreatedAt = CreatedAt;            
            return group;
        }
        private bool ValidateData(out string ErrorMessage)
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Name is empty";
                return false;
            }
            if(CreatedAt > DateTime.Now)
            {
                ErrorMessage = "Created at is in future";
                return false;
            }
            ErrorMessage = string.Empty;
            return true;
        }
        public async Task<IEnumerable<User>> LoadAllUsersAsync()
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repoUser = uow.Repository<User>();
            return await repoUser.GetAllAsync();
        }
        public async Task<IEnumerable<User>> LoadUsersForGroupAsync()
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<Group>();
            await repo.LoadAssociatedCollectionAsync(group, g => g.Users);
            return group.Users;
        }
        private void InitFields(Group group)
        {
            this.group = group;
            mode = ViewMode.Edit;
            Name += $" {group.Name}";
            Description = group.Description;
            IsArchived = group.IsArchived;
            CreatedAt = group.CreatedAt;
            Task.Run(async () => Users = new(await LoadUsersForGroupAsync()));
        }

        public async Task UpdateAsynk()
        {
            var users = await LoadAllUsersAsync();
            AllUsers = new(users);
        }
        #endregion Methods
    }
}
