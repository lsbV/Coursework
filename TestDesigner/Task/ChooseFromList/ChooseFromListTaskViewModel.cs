using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading;
using System.Windows.Input;
using TestDesigner.Answer;
using TestDesigner.Infrastructure;
using TestLib.Classes.Tasks;
using TestLib.Abstractions;
using System.Linq;

namespace TestDesigner.Task.ChooseFromList
{
    public partial class ChooseFromListTaskViewModel : BaseTaskViewModel
    {
        public ChooseFromListTaskViewModel() : this(new ChooseFromListTask())
        {
            workMode = WorkMode.Create;            
        }
        public ChooseFromListTaskViewModel(TestLib.Abstractions.Task task) : base(task) 
        {
            Name = "Choose from list";
            Description = "Choose from list";
        }
        public override TestLib.Abstractions.Task CreateTask()
        {
            return new ChooseFromListTask(Description, Point, Answers.ToList(),  Body);
        }
    }
}
