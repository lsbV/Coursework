using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDesigner.Infrastructure;
using TestDesigner.ViewLib;
using TestLib.Abstractions;

namespace TestDesigner.Task
{
    public abstract partial class BaseTaskViewModel : BaseViewModel
    {
        [ObservableProperty] private string description;
        [ObservableProperty] private double point;
        [ObservableProperty] private ICollection<TestLib.Abstractions.Answer> answers;
        //[ObservableProperty] private ICollection<IAnswer> correctAnswers;
        [ObservableProperty] private TestLib.Abstractions.Body body;
        protected WorkMode workMode;

        //public BaseTaskViewModel()
        //{
        //    workMode = WorkMode.Create;
        //}
        public BaseTaskViewModel(TestLib.Abstractions.Task task)
        {
            workMode = WorkMode.Edit;
            Description = task.Description;
            Point = task.Point;
            Answers = task.Answers;
            //CorrectAnswers = task.Answers.Where(a=>a.IsCorrect).ToList();
            Body = task.Body;
        }

        public abstract TestLib.Abstractions.Task CreateTask();
    }
}
