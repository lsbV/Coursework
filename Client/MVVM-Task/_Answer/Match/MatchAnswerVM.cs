// Ignore Spelling: MVVM

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TestLib.Abstractions;
using TestLib.Classes.Answers;

namespace Client.MVVM_Task._Answer
{
    public partial class MatchAnswerVM : BaseAnswerVM
    {
        [ObservableProperty] MatchAnswerVM? partner;
        public MatchAnswer Answer => (MatchAnswer)answer;
        //[ObservableProperty] bool isJoined;
        public MatchAnswerVM(Answer answer) : base(answer)
        {

        }

        [RelayCommand]
        void Join(object param)
        {
            var anotherVM = (MatchAnswerVM)param;
            if (anotherVM.Partner == null)
            {
                anotherVM.Partner = this;
                Partner = anotherVM;
            }
        }
        [RelayCommand]
        void Unjoin(object param)
        {
            var anotherVM = (MatchAnswerVM)param;
            if (anotherVM.Partner == null)
            {
                anotherVM.Partner = null;
                Partner = null;
            }
        }


        public override Answer GetAnswerResult()
        {
            return new MatchAnswer
            {
                Id = answer.Id,
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
                Partner = Partner?.Answer,
                PartnerId = Partner?.Answer?.Id ?? 0,
                Side = Answer.Side,
                TaskId = answer.TaskId,
                Task = null
            };
        }

    }
}
