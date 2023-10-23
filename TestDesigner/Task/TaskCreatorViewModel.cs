using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using TestDesigner.ViewLib;
using TestLib.Interfaces;
using TestLib.Classes.Bodies;
using TestLib.Classes.Answers;
using TestLib.Classes.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace TestDesigner.Task
{
    public partial class TaskCreatorViewModel : BaseViewModel
    {
        #region Fields
        [ObservableProperty] private string description;
        [ObservableProperty] private double point;
        [ObservableProperty] private ITaskBody body;
        [ObservableProperty] private ICollection<string> bodyTypes;
        [ObservableProperty] private string selectedBodyType;
        [ObservableProperty] private ICollection<string> taskTypes;
        [ObservableProperty] private string selectedTaskType;
        private ITask task;
        private ApplicationViewModel.ApplicationController controller;
        #endregion Fields

        #region Properties

        #endregion Properties

        #region Constructors
        public TaskCreatorViewModel(ApplicationViewModel.ApplicationController controller)
        {
            this.controller = controller;
            BodyTypes = new List<string>()
            {
                "Text", 
                "Image",
                //new TextTaskBody(),
                //new ImageTaskBody(),
            };
            TaskTypes = new List<string>()
            {
                "Choose answer",
                "Enter text",
                //new ChooseFromListTask(),
                //new EnterTextTask(),
            };
            //AddTaskCommand = new RelayCommand(CreateTask);
        }
        #endregion Constructors

        #region Commands
        //[ObservableProperty] private ICommand addTaskCommand;
        [RelayCommand/*(CanExecute =nameof(CanCreateTask))*/]
        private void CreateTask()
        {
            task = TaskCreator.CreateTask(SelectedTaskType, SelectedBodyType);
            Body = task.Body;
        }
        #endregion Commands

        #region Methods
        private bool CanCreateTask()
        {
            return SelectedBodyType != null && SelectedTaskType != null && task == null;
        }

        #endregion Methods
    }

    
}
