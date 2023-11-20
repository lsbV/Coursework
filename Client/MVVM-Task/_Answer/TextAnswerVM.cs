using TestLib.Abstractions;
using TestLib.Classes.Answers;

namespace Client.MVVM_Task._Answer
{
    public class TextAnswerVM : BaseAnswerVM
    {
        public TextAnswerVM(Answer answer) : base(answer)
        {
        }

        public override Answer GetAnswerResult()
        {
            return new TextAnswer()
            {
                Id = answer.Id,
                IsCorrect = IsCorrect,
                Text = Text
            };
        }
    }
}
