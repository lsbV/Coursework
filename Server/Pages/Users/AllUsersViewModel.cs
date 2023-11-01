using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DALTestsDB;
using Microsoft.EntityFrameworkCore;
using Server.Pages.Application;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Pages.Users
{
    public partial class AllUsersViewModel : BaseViewModel, IUpdateable
    {
        #region ObservableProperties
        [ObservableProperty] ObservableCollection<User> users;

        #endregion ObservableProperties

        #region Properties

        #endregion Properties

        #region Constructors
        public AllUsersViewModel()
        {
            Name = "Users";
            Users = new ObservableCollection<User>();           
        }
        #endregion Constructors

        #region Commands
        [RelayCommand]
        private async Task EditUser()
        {
            
        }
        #endregion Commands

        #region Methods
        public async Task LoadUsersAsynk()
        {
            using (var db = new TestDBContext())
            {
                Users = new(await db.User.ToArrayAsync());
            }
        }  

        public async Task UpdateAsynk()
        {
            await LoadUsersAsynk();
        }
        #endregion Methods

    }
}
