using TestLib.Abstractions;
using TestLib.Classes.Answers;

namespace Client.MVVM_Task._Answer
{
    public class EnterTextVM : BaseAnswerVM
    {
        public EnterTextVM(Answer answer) : base(answer)
        {
        }

        public override Answer GetAnswerResult()
        {
            return new EnterTextAnswer() { Id = 0, IsCorrect = true, Text = this.Text, TaskId = answer.TaskId, Task = null! };
        }
    }
}
