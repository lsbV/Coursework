using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DALTestsDB;
using Server.Pages.Application;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Interfaces;
using Xceed.Wpf.Toolkit;

namespace Server.Pages.Users
{
    public partial class UserViewModel : BaseViewModel
    {
        #region Fields
        private int id;
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
        #endregion ObservableProperties

        #region Properties
        public List<UserRole> Roles { get; }
        public ObservableCollection<UserGroup> UserGroups { get; }
        #endregion Properties

        #region Constructors
        public UserViewModel()
        {
            Name = "User";
            mode = ViewMode.Create;
            UserGroups = new ObservableCollection<UserGroup>();
            Roles = Enum.GetValues<UserRole>().ToList();
        }
        public UserViewModel(User user) : this()
        {
            mode = ViewMode.Edit;
            Name += $" {user.Login}";
            id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Role = user.Role;
            Login = user.Login;
            Password = user.Password;
            Description = user.Description;
            IsArchived = user.IsArchived;
            CreatedAt = user.CreatedAt;
            UserGroups = new ObservableCollection<UserGroup>(user.UserGroups);
        }
        #endregion Constructors

        #region Commands
        [RelayCommand] public async Task Save()
        {
            if (ValidateData(out string msg) == false)
            {
                MessageBox.Show(msg);
                return;
            }
            using (var db = new TestDBContext())
            {
                if(mode == ViewMode.Create)
                {
                    User user = CreateUser();
                    await db.AddAsync(user);
                }
                if(mode == ViewMode.Edit)
                {
                    User user = CreateUser();
                    user.Id = id;
                    db.Update(user);
                }
            }
        }
        #endregion Commands

        #region Methods
        private User CreateUser()
        {
            return new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Role = Role,
                Login = Login,
                Password = Password,
                Description = Description,
                IsArchived = IsArchived,
                CreatedAt = CreatedAt,
                UserGroups = UserGroups.ToList()
            };
        }

        private bool ValidateData(out string ErrorMessage)
        {
            if(string.IsNullOrWhiteSpace(FirstName))
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
            if(CreatedAt > DateTime.Now)
            {
                ErrorMessage = "Created at is in future";
                return false;
            }
            if(UserGroups.Count == 0)
            {
                ErrorMessage = "User must be in at least one group";
                return false;
            }
            ErrorMessage = string.Empty;
            return true;
        }

       
        #endregion Methods
    }
}
