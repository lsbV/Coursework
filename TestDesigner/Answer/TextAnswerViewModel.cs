using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDesigner.ViewLib;

namespace TestDesigner.Answer
{
    public partial class TextAnswerViewModel : BaseViewModel
    {
        [ObservableProperty] private string text;
        public TextAnswerViewModel()
        {
            Name = "Text answer";
        }
    }
}
