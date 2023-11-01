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

namespace Server.Pages.TestsAssigned
{
    public partial class TestsAssignedViewModel : BaseViewModel
    {
        #region ObservableProperties
        [ObservableProperty] ObservableCollection<TestAssignedUser> testsAssigned;

        #endregion ObservableProperties
        #region Properties
        #endregion Properties
        #region Constructors
        public TestsAssignedViewModel()
        {
            Name = "Tests assigned";
            TestsAssigned = new();
        }
        #endregion Constructors
        #region Commands
        #endregion Commands
        #region Methods
        public async Task LoadGroups()
        {
            using (var db = new TestDBContext())
            {
                TestsAssigned = new(await db.TestAssignedUser.ToArrayAsync());
            }
        }
        #endregion Methods
    }
}
