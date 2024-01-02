// Ignore Spelling: MVVM

using Client.MVVM_Task._Answer;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TestLib.Abstractions;
using TestLib.Classes.Answers;
using TestLib.Classes.Enums;
using TestLib.Classes.Tasks;

namespace Client.MVVM_Task._Task
{
    public partial class MatchTaskVM : BaseTaskVM
    {
        [ObservableProperty] MatchAnswerVM? selectedLeftItem;
        [ObservableProperty] MatchAnswerVM? selectedRightItem;

        public List<MatchAnswerVM> Left { get; set; }
        public List<MatchAnswerVM> Right { get; set; }


        public MatchTaskVM(Task task) : base(task)
        {
            Left = Answers.Cast<MatchAnswerVM>().Where(a => a.Answer.Side == MatchSide.Left).ToList();
            Right = Answers.Cast<MatchAnswerVM>().Where(a => a.Answer.Side == MatchSide.Right).ToList();
            SelectedLeftItem = Left.FirstOrDefault();
            SelectedRightItem = Right.FirstOrDefault();
        }

        partial void OnSelectedLeftItemChanged(MatchAnswerVM? value)
        {
            if (SelectedLeftItem != null && SelectedRightItem != null)
            {
                SelectedLeftItem.JoinCommand.Execute(SelectedRightItem);
                //SelectedLeftItem.Partner = SelectedRightItem;
                //SelectedRightItem.Partner = SelectedLeftItem;
                SelectedLeftItem = null;
                SelectedRightItem = null;
            }
        }
        partial void OnSelectedRightItemChanged(MatchAnswerVM? value)
        {
            if (SelectedLeftItem != null && SelectedRightItem != null)
            {
                SelectedRightItem.JoinCommand.Execute(SelectedLeftItem);
                //SelectedLeftItem.Partner = SelectedRightItem;
                //SelectedRightItem.Partner = SelectedLeftItem;
                SelectedLeftItem = null;
                SelectedRightItem = null;
            }
        }



        public override Task GetTaskResult()
        {
            return new MatchTask
            {
                Id = task.Id,
                Body = null!,
                BodyId = task.BodyId,
                Description = null!,
                Point = task.Point,
                Test = null!,
                TestId = task.TestId,
                Answers = Left.Concat(Right).Where(vm => vm.Partner != null).Select(vm => vm.GetAnswerResult()).ToList()
            };
        }
    }
}
