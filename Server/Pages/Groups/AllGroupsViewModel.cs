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

namespace Server.Pages.Groups
{
    public partial class AllGroupsViewModel : BaseViewModel
    {
        #region ObservableProperties
        [ObservableProperty] ObservableCollection<Group> groups;

        #endregion ObservableProperties
        #region Properties
        #endregion Properties
        #region Constructors
        public AllGroupsViewModel()
        {
            Name = "Groups";
            Groups = new ();
        }
        #endregion Constructors
        #region Commands
        #endregion Commands
        #region Methods
        public async Task LoadGroups()
        {
            using (var db = new TestDBContext())
            {
                Groups = new(await db.Group.ToArrayAsync());
            }
        }
        #endregion Methods
    }
}
