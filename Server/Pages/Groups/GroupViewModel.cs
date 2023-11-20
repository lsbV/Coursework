using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Repository;
using Server.Ninject;
using Server.Pages.Application;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TestLib;

namespace Server.Pages.Groups
{
    public partial class GroupViewModel : BaseViewModel, IUpdateable
    {
        #region Fields
        private Group group;
        private IMessenger messenger;
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
        

        #region Constructors
        public GroupViewModel(IMessenger messenger)
        {
            this.group = null!;
            base.Name = "New group";
            this.mode = ViewMode.Create;
            this.Users = new ();
            this.AllUsers = null!;
            this.messenger = messenger;
        }
        public GroupViewModel(Group group, IMessenger messenger) : this(messenger)
        {
            base.Name = $"Group {group.Name}";
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
            Group group = await CreateGroup(uow);
            if (mode == ViewMode.Create)
            {
                await repoGroup.AddAsync(group);
            }
            else if(mode == ViewMode.Edit)
            {
                await repoGroup.UpdateAsync(group);
            }
            messenger.Send(group);
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

        [RelayCommand]
        private void Cancel()
        {
            messenger.Send(new Group());
        }
        #endregion Commands


        #region Methods
        private async Task<Group> CreateGroup(IGenericUnitOfWork uow)
        {
            Group group = this.group ?? new();
            group.Name = this.Name;
            group.Description = Description;
            group.IsArchived = IsArchived;
            group.CreatedAt = CreatedAt;
            if(mode == ViewMode.Create)
            {
                group.Users = Users.ToList();
                var usersId = Users.Select(u => u.Id).ToArray();
                var newUsers = await uow.Repository<User>().FindAllAsync(x => usersId.Contains(x.Id));
                group.Users = newUsers.ToList();
            }
            else if(mode == ViewMode.Edit)
            {
                var usersId = Users.Select(u => u.Id).ToArray();
                var newUsers = await uow.Repository<User>().FindAllAsync(x=>usersId.Contains(x.Id));
                group.Users = null!;
                await uow.Repository<Group>().LoadAssociatedCollectionAsync(group, g => g.Users);
                group.Users.Clear();
                group.Users.AddRange(newUsers);    
            }
            return group;
        }

        private bool ValidateData(out string ErrorMessage)
        {
            if(string.IsNullOrWhiteSpace(this.Name))
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

        public static async Task<IEnumerable<User>> LoadAllUsersAsync()
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repoUser = uow.Repository<User>();
            return await repoUser.GetAllAsync();
        }

        public IEnumerable<User> LoadUsersForGroup()
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<Group>();
            repo.LoadAssociatedCollection(group, g => g.Users);
            return group.Users;
        }
        private void InitFields(Group group)
        {
            this.group = group;
            this.mode = ViewMode.Edit;
            this.Name += group.Name;
            this.Description = group.Description;
            this.IsArchived = group.IsArchived;
            this.CreatedAt = group.CreatedAt;
            Task.Run(() => this.Users = new(LoadUsersForGroup()));
        }

        public async Task UpdateAsynk()
        {
            var users = await LoadAllUsersAsync();
            AllUsers = new(users);
        }
        #endregion Methods
    }
}
