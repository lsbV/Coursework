using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Classes.Tasks;

namespace Client.MVVM_Task._Task
{
    public class MultipleSelectVM : BaseTaskVM
    {
        public MultipleSelectVM(TestLib.Abstractions.Task task) : base(task)
        {
        }

        public override TestLib.Abstractions.Task GetTaskResult()
        {
            return new MultipleSelectTask()
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
