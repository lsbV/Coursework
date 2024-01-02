using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;
using TestLib.Abstractions;
using TestLib.Classes.Tasks;

namespace Client.MVVM_Task._Task
{
    public partial class EnterTextTaskVM : BaseTaskVM
    {
        [ObservableProperty] public Answer answer;
        public EnterTextTaskVM(Task task) : base(task)
        {
            Answer = task.Answers[0];
        }

        public override Task GetTaskResult()
        {
            return new EnterTextTask()
            {
                Id = 0,
                Answers = this.Answers.Select(answer => answer.GetAnswerResult()).ToList(),
                BodyId = this.task.BodyId,
                TestId = this.task.TestId,
                Point = this.task.Point,
                Description = null!,
                Body = null!,
                Test = null!,
            };
        }
    }
}
