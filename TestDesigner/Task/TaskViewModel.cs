using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using TestDesigner.ViewLib;
using TestLib.Interfaces;
using CommunityToolkit.Mvvm.Input;
using TestDesigner.Body;
using TestDesigner.Task.ChooseFromList;
using System.Linq;
using TestDesigner.Answer;
using System.Windows.Input;

namespace TestDesigner.Task
{
    public partial class TaskViewModel : BaseViewModel
    {
        #region Fields
        private AnswerRepository answerRepository;
        private ApplicationViewModel.ApplicationController controller;
        [ObservableProperty] private string description;
        [ObservableProperty] private double point;
        [ObservableProperty] private ICollection<BaseTaskViewModel> taskTypes;
        [ObservableProperty] private BaseTaskViewModel selectedTaskType;
        [ObservableProperty] private BodyCreatorViewModel body;
        [ObservableProperty] private AnswerViewModel answer;
        #endregion Fields

        #region Properties

        #endregion Properties

        #region Commands
        [ObservableProperty] private ICommand addAnswerCommand;
        [ObservableProperty] private ICommand saveCommand;
        [ObservableProperty] private ICommand cancelCommand;
        #endregion Commands

        #region Constructors
        public TaskViewModel(ApplicationViewModel.ApplicationController controller)
        {
            AddAnswerCommand = new RelayCommand(AddAnswer);
            SaveCommand = new RelayCommand(Save);
            this.controller = controller;
            Body = new BodyCreatorViewModel();
            Answer = new AnswerViewModel();
            answerRepository = AnswerRepository.Instance;
            TaskTypes = new List<BaseTaskViewModel>()
            {
                new ChooseFromListTaskViewModel(),
            };
            SelectedTaskType = TaskTypes.First();
        }
        #endregion Constructors


        #region Methods
        public void AddAnswer()
        {
            SelectedTaskType.Answers.Add(Answer.CreateAnswer());
        }
        public void Save()
        {
            var task = SelectedTaskType.CreateTask();
            task.Point = Point;
            task.Body = Body.CreateBody();
            controller.AddTask(task);
            controller.SetDefoultView();
        }
        public void Cancel()
        {
            controller.SetDefoultView();
        }
        partial void OnSelectedTaskTypeChanged(BaseTaskViewModel value)
        {
            Description = value.Description;
            Answer.AnswerTypes = answerRepository.GetAnswerTypes(value.GetType()).ToList();
            Answer.SelectedAnswerType = Answer.AnswerTypes.First();
        }
        #endregion Methods
    }

    
}
