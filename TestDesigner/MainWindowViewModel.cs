using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TestDesigner.Infrastructure;
using TestDesigner.Task;
using TestDesigner.ViewLib;
using TestLib.Interfaces;
using Xceed.Wpf.Toolkit;

namespace TestDesigner
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private string path;
        [ObservableProperty] private string title;
        [ObservableProperty] private string author;
        [ObservableProperty] private string description;
        [ObservableProperty] private string infoForTestTaker;
        [ObservableProperty] private int countOfTasks;
        [ObservableProperty] private double maxPoints;
        [ObservableProperty] private double minPoints;

        [ObservableProperty] private ObservableCollection<IAnswer> answers;
        [ObservableProperty] private ObservableCollection<ITask> tasks;
        [ObservableProperty] private ITask selectedTask;
        [ObservableProperty] private IAnswer selectedAnswer;
        #endregion Fields

        #region Properties
        public ApplicationViewModel.ApplicationController Controller { get; set; }
        public IFileExplorerProvider FileExplorerProvider { get; set; }
        #endregion Properties

        #region Constructors
        public MainWindowViewModel()
        {   
            Answers = new ObservableCollection<IAnswer>();
            Tasks = new ObservableCollection<ITask>();
            FileExplorerProvider = new FileExplorer();
            Answers.CollectionChanged += ((s,e) =>  Recount());
        }
        #endregion Constructors

        #region Commands
        [RelayCommand] private void CreateEditTask()
        {
            Controller.ChangeView(new TaskViewModel(Controller));
        }
        [ObservableProperty] private ICommand deleteTaskCommand;
        [RelayCommand] private void SaveTest()
        {
            ITest test = new TestLib.Classes.Test.Test()
            {
                Title = Title,
                Author = Author,
                Description = Description,
                InfoForTestTaker = InfoForTestTaker,
                Tasks = Tasks.ToArray(),
                MinPoints = MinPoints,
                MaxPoints = MaxPoints
            };
            var isSaved = FileExplorerProvider.SaveFile(test, path);
            if (isSaved)
            {
                MessageBox.Show("Test saved");
            }
            else
            {
                MessageBox.Show("Test not saved");
            }
        }
        [RelayCommand] private void LoadTest()
        {
            path = FileExplorerProvider.OpenFileDialog();
            if(path != null)
            {
                try
                {
                    var test = FileExplorerProvider.OpenFile(path);
                    if (test != null)
                    {
                        Title = test.Title;
                        Author = test.Author;
                        Description = test.Description;
                        InfoForTestTaker = test.InfoForTestTaker;
                        Tasks = new ObservableCollection<ITask>(test.Tasks);
                        MinPoints = test.MinPoints;
                        MaxPoints = test.MaxPoints;
                        Recount();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }
        #endregion Commands

        #region Methods
        public void Recount()
        {
            CountOfTasks = Tasks.Count;
            var max = 0.0;
            foreach (var item in Tasks) { max += item.Point; }
            MaxPoints = max;
        }
        partial void OnSelectedTaskChanged(ITask value)
        {
            Answers = new(SelectedTask.Answers);
        }
        #endregion Methods
    }
}
