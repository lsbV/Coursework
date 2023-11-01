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

namespace Server.Pages.Tests
{
    public partial class AllTestsViewModel : BaseViewModel
    {
        #region ObservableProperties
        [ObservableProperty] ObservableCollection<Test> tests;

        #endregion ObservableProperties
        #region Properties
        #endregion Properties
        #region Constructors
        public AllTestsViewModel()
        {
            Name = "Groups";
            Tests = new();
        }
        #endregion Constructors
        #region Commands
        #endregion Commands
        #region Methods
        public async Task LoadGroups()
        {
            using (var db = new TestDBContext())
            {
                Tests = new(await db.Test.ToArrayAsync());
            }
        }
        #endregion Methods
    }
}
