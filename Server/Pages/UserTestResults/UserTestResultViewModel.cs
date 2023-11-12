using CommunityToolkit.Mvvm.ComponentModel;
using DALTestsDB;
using Microsoft.EntityFrameworkCore;
using Server.Pages.Application;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Pages.UserTestResults
{
    public class AllUserTestResultsViewModel : BaseViewModel
    {
        #region ObservableProperties
        //[ObservableProperty] ObservableCollection<UserTestResult> tests;
        #endregion ObservableProperties
        #region Properties
        #endregion Properties
        #region Constructors
        public AllUserTestResultsViewModel()
        {
            Name = "User test result";
            //Tests = new ();
        }
        #endregion Constructors
        #region Commands
        #endregion Commands
        #region Methods
        //public async Task LoadData()
        //{
        //    //using (var db = new TestDBContext())
        //    //{
        //    //    //Tests = new(await db.UserTest.TakeLast(20).ToArrayAsync());
        //    //}
        //}
        #endregion Methods
    }
}
