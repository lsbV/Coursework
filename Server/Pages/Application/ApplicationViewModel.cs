using CommunityToolkit.Mvvm.ComponentModel;
using Server.Pages.Users;
using SharpRepository.EfRepository;
using SharpRepository.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            current = pages.First();
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
