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
        public override object Clone()
        {
            return new EnterTextAnswer() { Id = Id, IsCorrect = IsCorrect, Text = Text, TaskId = TaskId, Task = null! };
        }

        public override int CompareTo(Answer? other)
        {
            throw new NotImplementedException();
        }

        public override Answer GetClearAnswer()
        {
            throw new NotImplementedException();
        }
    }
}
