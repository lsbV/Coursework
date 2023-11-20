using Client.Infrastructure;
using Client.MVVM_Task._Answer;
using Client.MVVM_Task._Body;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using TestLib.Abstractions;

namespace Client.MVVM_Task._Task
{
    public abstract partial class BaseTaskVM : BaseViewModel
    {
        protected Task task;

        [ObservableProperty] int id;
        [ObservableProperty] string description;
        [ObservableProperty] BaseBodyVM body;
        [ObservableProperty] List<BaseAnswerVM> answers;

        protected BaseTaskVM(Task task)
        {
            this.task = task;
            Description = task.Description;
            Body = task.Body.GetBodyViewModel();
            Answers = task.Answers.Select(a => a.GetAnswerViewModel()).ToList();
        }

        public abstract Task GetTaskResult();
    }
}
