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
    public partial class AnswerViewModel : BaseViewModel
    {
        #region Fields
        [ObservableProperty] private List<BaseAnswerViewModel> answerTypes;
        [ObservableProperty] private BaseAnswerViewModel selectedAnswerType;
        #endregion Fields

        #region Properties
        #endregion Properties

        #region Constructors
        public AnswerViewModel()
        {
            AnswerTypes = new List<BaseAnswerViewModel>()
            {
                new TextAnswerViewModel(),
            };
            SelectedAnswerType = AnswerTypes.First();
        }
        #endregion Constructors

        #region Commands
        #endregion Commands

        #region Methods
        public IAnswer CreateAnswer()
        {
            return SelectedAnswerType.CreateAnswer();
        }
        #endregion Methods
    }
}
