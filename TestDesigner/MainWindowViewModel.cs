using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows.Input;
using TestDesigner.Task;
using TestDesigner.ViewLib;
using TestLib.Interfaces;

namespace TestDesigner
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        [ObservableProperty] private string title;
        [ObservableProperty] private string author;
        [ObservableProperty] private string description;
        [ObservableProperty] private string infoForTestTaker;
        [ObservableProperty] private int countOfTasks;
        [ObservableProperty] private double maxPoints;
        [ObservableProperty] private double minPoints;

        [ObservableProperty] private ICollection<IAnswer> answers;
        [ObservableProperty] private ICollection<ITask> tasks;
        [ObservableProperty] private ITask selectedTask;
        [ObservableProperty] private IAnswer selectedAnswer;

        private ApplicationViewModel.ApplicationController controller;
        #endregion Fields

        #region Properties

        #endregion Properties

        #region Commands
        [RelayCommand] private void AddTask()
        {
            controller.ChangeView(new TaskCreatorViewModel(controller));
        }
        [ObservableProperty] private ICommand deleteTaskCommand;
        [ObservableProperty] private ICommand addAnswerCommand;
        [ObservableProperty] private ICommand deleteAnswerCommand;
        [ObservableProperty] private ICommand saveTestCommand;
        [ObservableProperty] private ICommand loadTestCommand;
        #endregion Commands

        #region Constructors
        public MainWindowViewModel(ApplicationViewModel.ApplicationController controller)
        {   
            this.controller = controller;
            //Tasks = new ObservableCollection<ITask>()
            //{
            //    new ChooseFromListTask(){Text = "Choose from list", Point = 1, Answers = new List<IAnswer>(){new TextAnswer() { Text = "Answer 1"}, new TextAnswer() { Text = "Answer 2"}}},
            //};
            //JsonSerializerSettings settings = new JsonSerializerSettings
            //{
            //    TypeNameHandling = TypeNameHandling.All
            //};
            //string json = JsonConvert.SerializeObject(Tasks, settings);
            //var res = JsonConvert.DeserializeObject<ICollection<ITask>>(json,settings);
            //MessageBox.Show(res.First().Body.Text);
        }
        #endregion Constructors

        #region Methods
        
        #endregion Methods
    }
}
