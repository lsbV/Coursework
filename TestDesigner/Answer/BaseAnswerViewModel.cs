using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDesigner.ViewLib;
using TestLib.Interfaces;

namespace TestDesigner.Answer
{
    public abstract partial class BaseAnswerViewModel : BaseViewModel, ICloneable
    {
        [ObservableProperty] private bool isCorrect;
        [ObservableProperty] private string text;

        public abstract object Clone();

        public abstract IAnswer CreateAnswer();
    }
}
