﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDesigner.ViewLib;
using TestLib.Abstractions;

namespace TestDesigner.Body
{
    public partial class BodyCreatorViewModel : BaseViewModel
    {
        [ObservableProperty] private ICollection<BaseBodyViewModel> bodyTypes;
        [ObservableProperty] private BaseBodyViewModel selectedViewModel;
        public BodyCreatorViewModel()
        {
            Name = "BodyCreator";
            BodyTypes = new List<BaseBodyViewModel>()
            {
                new TextTaskBodyViewModel(),
            };
            SelectedViewModel = BodyTypes.First();
        }
        public TestLib.Abstractions.Body CreateBody()
        {
            return SelectedViewModel.CreateBody();
        }
    }
}
