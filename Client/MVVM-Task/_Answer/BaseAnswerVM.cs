using Client.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;

namespace Client.MVVM_Task._Answer
{
    public abstract partial class BaseAnswerVM : BaseViewModel
    {
        protected Answer answer;

        [ObservableProperty] string text;
        [ObservableProperty] bool isCorrect;

        public BaseAnswerVM(Answer answer)
        {
            this.answer = answer;
            Text = answer.Text;
            IsCorrect = answer.IsCorrect;
        }

        public abstract Answer GetAnswerResult();
    }
}
