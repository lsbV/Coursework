using System.Linq;
using TestLib.Classes.Tasks;

namespace Client.MVVM_Task._Task
{
    public partial class ChooseFromListVM : BaseTaskVM
    {  
        
        public ChooseFromListVM(TestLib.Abstractions.Task task) : base(task)
        {            
            
        }

        public override TestLib.Abstractions.Task GetTaskResult()
        {
            return new ChooseFromListTask()
            {
                Id = task.Id,
                Body = null!,
                TestId = task.TestId,
                BodyId = task.BodyId,
                Description = null!,
                Point = task.Point,
                Test = null!,
                Answers = Answers.Where(a => a.IsCorrect).Select(a => a.GetAnswerResult()).ToList()
            };
        }
    }
}
