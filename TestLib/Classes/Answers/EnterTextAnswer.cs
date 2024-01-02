using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;

namespace TestLib.Classes.Answers
{
    public class EnterTextAnswer : Answer
    {
        public override int CompareTo(Answer? other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            if (other is not EnterTextAnswer textAnswer)
            {
                throw new ArgumentException("Incorrect type of answer");
            }
            return Text.CompareTo(textAnswer.Text);
        }

        public override Answer GetClearAnswer()
        {
            return new EnterTextAnswer() { Id = Id, IsCorrect = false, Text = string.Empty, TaskId = TaskId, Task = null! };
        }
    }
}
