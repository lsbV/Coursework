using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Repository;
using Server.Ninject;
using Server.Pages.Application;
using System;
using System.Threading.Tasks;
using TestLib.Abstractions;
using TestLib.Classes.Test;
using Xceed.Wpf.Toolkit;

namespace Server.Pages.Tests
{
    public partial class TestViewModel : BaseViewModel
    {

        IMessenger messenger;
        
        #region ObservableProperties
        [ObservableProperty] string title = string.Empty;
        [ObservableProperty] string description = string.Empty;
        [ObservableProperty] string infoForTestTaker = string.Empty;
        [ObservableProperty] string author = string.Empty;
        [ObservableProperty] DateTime creationDate;
        [ObservableProperty] double passingPercent;
        [ObservableProperty] bool isArchived;
        [ObservableProperty] ViewMode viewMode = ViewMode.View;
        [ObservableProperty] Test? test;
        #endregion ObservableProperties

        #region Constructors
        public TestViewModel(Test test, IMessenger messenger)
        {
            ViewMode = ViewMode.Edit;
            this.messenger = messenger;
            InitFields(test);
        }
        public TestViewModel(IMessenger messenger)
        {
            ViewMode = ViewMode.Create;
            this.messenger = messenger;
        }

        #endregion Constructors


        #region Commands
        [RelayCommand(CanExecute = nameof(CanLoad))]
        private void Load(object param)
        {
            IFileExplorerProvider fileExplorerProvider = DI.Create<IFileExplorerProvider>();
            var path = fileExplorerProvider.OpenFileDialog();
            if (path != null)
            {
                try
                {
                    var test = fileExplorerProvider.OpenFile<Test>(path);
                    InitFields(test);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async System.Threading.Tasks.Task Save(object param)
        {
            if (Validate(out string msg) == false)
            {
                MessageBox.Show(msg);
                return;
            }
            using var ouw = DI.Create<IGenericUnitOfWork>();
            var repo = ouw.Repository<Test>();
            var test = InitTest(Test!);
            if (ViewMode == ViewMode.Create)
            {
                await repo.AddAsync(Test!);
            }
            else if(ViewMode == ViewMode.Edit)
            {
                await repo.UpdateAsync(test);
            }
        }

        [RelayCommand]
        private void Cancel(object param)
        {
            
        }

        [RelayCommand]
        private void Download()
        {
            ISerializer serializer = DI.Create<ISerializer>();
            IFileExplorerProvider fileExplorerProvider = DI.Create<IFileExplorerProvider>();
            var path = fileExplorerProvider.SaveFileDialog();
            if (path != null)
            {
                try
                {
                    var serializedTest = serializer.Serialize(Test!);
                    fileExplorerProvider.SaveFile(Test!, path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion Commands



        #region Methods
        private Test InitTest(Test test)
        {
            test.Title = Title;
            test.Description = Description;
            test.InfoForTestTaker = InfoForTestTaker;
            test.Author = Author;
            test.CreatedAt = CreationDate;
            test.PassingPercent = PassingPercent;
            test.IsArchived = IsArchived;
            return test;
        }
        private void InitFields(Test test)
        {
            this.Test = test;
            Name = $"Test: {test.Title}";
            Title = test.Title;
            Description = test.Description;
            InfoForTestTaker = test.InfoForTestTaker;
            Author = test.Author;
            CreationDate = test.CreatedAt;
            PassingPercent = test.PassingPercent;
            IsArchived = test.IsArchived;
        }
        private bool CanLoad()
        {
            if(Test == null)
            {
                return true;
            }
            return false;
        }
        private bool CanSave()
        {
            if(Test == null)
            {
                return false;
            }
            return true;
        }
        private bool Validate(out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                errorMessage = "Title is empty";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Description))
            {
                errorMessage = "Description is empty";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Author))
            {
                errorMessage = "Author is empty";
                return false;
            }
            if (CreationDate > DateTime.Now)
            {
                errorMessage = "CreationDate is in future";
                return false;
            }
            if (PassingPercent < 0 || PassingPercent > 100)
            {
                errorMessage = "PassingPercent is not in range [0, 100]";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }
        #endregion Methods
    }
}