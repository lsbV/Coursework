using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDesigner.ViewLib;
using TestLib.Classes.Answers;
using TestLib.Interfaces;

namespace TestDesigner.Answer
{
    public partial class TextAnswerViewModel : BaseAnswerViewModel
    {
        public TextAnswerViewModel()
        {
            Name = "Text answer";
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override IAnswer CreateAnswer()
        {
            return new TextAnswer(IsCorrect, Text);
        }
    }
}
