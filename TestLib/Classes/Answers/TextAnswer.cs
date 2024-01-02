using TestLib.Abstractions;
using TestLib.Classes.Exceptions;

namespace TestLib.Classes.Answers
{
    public class TextAnswer : Answer
    {
        public override int CompareTo(Answer? other)
        {
            if (other == null)
            {
                throw new System.ArgumentNullException(nameof(other));
            }
            else if (other is TextAnswer textAnswer)
            {
                return Text.CompareTo(textAnswer.Text);
            }
            else
            {
                throw new InvalidTypeException("Object is not a TextAnswer");
            }
        }

        public override Answer GetClearAnswer()
        {
            return new TextAnswer() { TaskId = TaskId, Id = Id, Text = Text, IsCorrect = false };
        }
    }
}
