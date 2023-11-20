using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;
using TestLib.Classes.Enums;

namespace TestLib.Classes.Answers
{
    internal class MatchAnswer : Answer
    {
        public MatchSide Side { get; set; }
        public int? PartnerId { get; set; }

        public MatchAnswer? Partner { get; set; }


        public override object Clone()
        {
            return new MatchAnswer() { Id = Id, IsCorrect = IsCorrect, TaskId = TaskId, Text = Text, Task = null, Side = Side, PartnerId = PartnerId, Partner = null };
        }

        public override int CompareTo(Answer? other)
        {
            if (this.Text == other?.Text && this.Partner?.Text == (other as MatchAnswer)?.Partner?.Text)
                return 0;
            return 1;
        }

        public override Answer GetClearAnswer()
        {
            return new MatchAnswer() { Id = Id, IsCorrect = false, TaskId = TaskId, Text = Text, Side = Side, Task = null, Partner = null, PartnerId = null };
        }
    }
}
