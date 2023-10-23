using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDesigner.ViewLib;
using TestLib;

namespace TestDesigner
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private string _title = "Test Designer";
        private ICollection<IAnswer> answers;
        #endregion Fields

        #region Properties
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public ICollection<ITask> Tasks { get; private set; }
        public ITask SelectedTask { get; set; }
        public ICollection<IAnswer> Answers { get => answers; private set => answers = value; }

        #endregion Properties

        #region Commands
        #endregion Commands

        #region Constructors
        public MainWindowViewModel()
        {
            Tasks = new ObservableCollection<ITask>();
            
        }
        #endregion Constructors

        #region Methods
        protected override void OnDispose()
        {
            throw new NotImplementedException();
        }
        #endregion Methods
    }
}
