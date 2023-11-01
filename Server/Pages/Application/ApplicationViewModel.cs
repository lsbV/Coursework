using CommunityToolkit.Mvvm.ComponentModel;
using Server.Pages.Groups;
using Server.Pages.Tests;
using Server.Pages.TestsAssigned;
using Server.Pages.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Application
{
    public partial class ApplicationViewModel : BaseViewModel
    {
        [ObservableProperty] ICollection<BaseViewModel> pages;
        [ObservableProperty] BaseViewModel current;

        ApplicationController controller;
        public ApplicationViewModel()
        {            
            Name = "Application";
            pages = new List<BaseViewModel>();
            controller = new ApplicationController(this);
            pages.Add(new AllUsersViewModel());
            pages.Add(new AllGroupsViewModel());
            pages.Add(new AllTestsViewModel());
            pages.Add(new TestsAssignedViewModel());
            current = pages.First();
        }

        partial void OnCurrentChanged(BaseViewModel viewModel)
        {
            if(viewModel is IUpdateable updateable)
            {
                Task.Run(updateable.UpdateAsynk);
            }
        }

        public class ApplicationController
        {
            ApplicationViewModel model;

            public ApplicationController(ApplicationViewModel model)
            {
                this.model = model;
            }

            public void ChangeView(BaseViewModel view)
            {
                if (view == null)
                    throw new ArgumentNullException(nameof(view));
                model.Current = view;
            }
        }
    }
}
