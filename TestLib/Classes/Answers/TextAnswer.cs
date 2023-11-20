using TestLib.Abstractions;

namespace TestLib.Classes.Answers
{
    public class TextAnswer : Answer
    { 
        public TextAnswer(bool isCorrect, string text)
        {
            IsCorrect = isCorrect;
            Text = text;
        }
        public TextAnswer()
        {
        }

        public override object Clone()
        {
            return new TextAnswer() { Id = Id, Text = Text, TaskId = TaskId, IsCorrect = IsCorrect };
        }

        public override int CompareTo(Answer? other)
        {
            if (other == null)
            {
                throw new System.ArgumentNullException(nameof(other));
            }
            else if(other is TextAnswer textAnswer)
            {
                return Text.CompareTo(textAnswer.Text);
            }
            else
            {
                throw new System.ArgumentException("Object is not a TextAnswer");
            }
        }

        public override Answer GetClearAnswer()
        {
            return new TextAnswer() { TaskId = TaskId, Id = Id, Text = Text, IsCorrect = false };
        }
    }
}
