using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DALTestsDB;
using Repository;
using Server.Ninject;
using Server.Pages.Application;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestLib.Interfaces;
using Xceed.Wpf.Toolkit;

namespace Server.Pages.Users
{
    public partial class UserViewModel : BaseViewModel, IUpdateable
    {
        #region Fields
        private int id;
        private User user = null!;
        private ViewMode mode;
        #endregion Fields

        #region ObservableProperties
        [ObservableProperty] string firstName = string.Empty;
        [ObservableProperty] string lastName = string.Empty;
        [ObservableProperty] UserRole role = UserRole.User;
        [ObservableProperty] string login = string.Empty;
        [ObservableProperty] string password = string.Empty;
        [ObservableProperty] string? description = string.Empty;
        [ObservableProperty] bool isArchived = false;
        [ObservableProperty] DateTime createdAt = DateTime.Now;
        [ObservableProperty] ObservableCollection<Group> userGroups;
        [ObservableProperty] ObservableCollection<Group> allGroups;
        #endregion ObservableProperties

        #region Properties
        public List<UserRole> Roles { get; }
        #endregion Properties

        #region Constructors
        public UserViewModel()
        {
            Name = "User";
            mode = ViewMode.Create;
            UserGroups = new();
            AllGroups = new();
            Roles = Enum.GetValues<UserRole>().ToList();
        }
        public UserViewModel(User user) : this()
        {
            this.user = user;
            mode = ViewMode.Edit;
            Name += $" {user.Login}";
            InitFields(user);
        }


        #endregion Constructors

        #region Commands
        [RelayCommand]
        private async Task Save()
        {
            if (ValidateData(out string msg) == false)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show(msg);
                return;
            }
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repoUser = uow.Repository<User>();
            var repoGroup = uow.Repository<Group>();
            if (mode == ViewMode.Create)
            {
                user = CreateUser(new User());
                user.Groups = await LoadGroupsForUser(repoGroup);
                await repoUser.AddAsync(user);
            }
            else
            {
                User? user = await repoUser.FindByIdAsync(this.user.Id);
                if (user == null)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("User not found");
                    return;
                }
                user = CreateUser(user);
                await repoUser.LoadAssociatedCollectionAsync(user, u => u.Groups);
                List<Group> groups = await LoadGroupsForUser(repoGroup);
                user.Groups.Clear();
                user.Groups.AddRange(groups);
                await repoUser.UpdateAsync(user);
            }


            WeakReferenceMessenger.Default.Send(user);
        }

        

        [RelayCommand]
        private void Cancel()
        {
            throw new NotImplementedException();
            //WeakReferenceMessenger.Default.Send();
        }
        [RelayCommand]
        private void Add(object param)
        {
            Group group = (Group)param;
            if (UserGroups.Contains(group))
            {
                return;
            }
            UserGroups.Add(group);
        }
        [RelayCommand]
        private void Remove(object param)
        {
            Group group = (Group)param;
            UserGroups.Remove(group);
        }
        #endregion Commands

        #region Methods
        private async Task<List<Group>> LoadGroupsForUser(IGenericRepository<Group> repoGroup)
        {
            var listId = UserGroups.Select(x => x.Id).ToList();
            var groups = new List<Group>(await repoGroup.FindAllAsync(x => listId.Contains(x.Id)));
            return groups;
        }
        private async Task<IEnumerable<Group>> LoadAllGroupsAsync()
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repo = uow.Repository<Group>();
            return await repo.GetAllAsync();
        }
        private async Task<IEnumerable<Group>> LoadAllGroupsForUserAsync(int id)
        {
            using var uow = DI.Create<IGenericUnitOfWork>();
            var repoUsers = uow.Repository<User>();
            await repoUsers.LoadAssociatedCollectionAsync(this.user, u => u.Groups);
            return user.Groups;
        }
        private User CreateUser(User newUser)
        {
            newUser.FirstName = FirstName;
            newUser.LastName = LastName;
            newUser.Role = Role;
            newUser.Login = Login;
            newUser.Password = Password;
            newUser.Description = Description;
            newUser.IsArchived = IsArchived;
            newUser.CreatedAt = CreatedAt;
            return newUser;
        }
        private void InitFields(User user)
        {
            id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Role = user.Role;
            Login = user.Login;
            Password = user.Password;
            Description = user.Description;
            IsArchived = user.IsArchived;
            CreatedAt = user.CreatedAt;
            if (mode == ViewMode.Edit)
            {
                //Task.Run(() => UserGroups = new(LoadAllGroupsForUser(user.Id).ToList()));
                Task.Run(async () => UserGroups = new(await LoadAllGroupsForUserAsync(user.Id)));
            }
            else
            {
                UserGroups = new();
            }
        }
        private bool ValidateData(out string ErrorMessage)
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                ErrorMessage = "First name is empty";
                return false;
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                ErrorMessage = "Last name is empty";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Login))
            {
                ErrorMessage = "Login is empty";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Password is empty";
                return false;
            }
            if (CreatedAt > DateTime.Now)
            {
                ErrorMessage = "Created at is in future";
                return false;
            }
            if (UserGroups.Count == 0)
            {
                ErrorMessage = "User must be in at least one group";
                return false;
            }
            ErrorMessage = string.Empty;
            return true;
        }

        public async Task UpdateAsynk()
        {
            var groups = await LoadAllGroupsAsync();
            AllGroups = new(groups);
        }


        #endregion Methods
    }
}
