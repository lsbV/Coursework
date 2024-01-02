using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;
using TestLib.Classes.Enums;

namespace TestLib.Classes.Answers
{
    public class MatchAnswer : Answer
    {
        public MatchSide Side { get; set; }
        public MatchAnswer Partner { get; set; } = null!;
        public int? PartnerId { get; set; }


        //public override object Clone()
        //{
        //    return new MatchAnswer() { Id = Id, IsCorrect = IsCorrect, TaskId = TaskId, Text = Text, Task = null, Side = Side, PartnerId = PartnerId, Partner = null! };
        //}
        public override Answer ClearDbData()
        {
            base.ClearDbData();
            this.PartnerId = null;
            if (this.Partner != null && this.Partner.PartnerId != null)
            {
                this.Partner.ClearDbData();
            }
            return this;
        }

        public override int CompareTo(Answer? other)
        {
            if (other == null)
            {
                return 1;
            }
            if (other is not MatchAnswer matchAnswer)
            {
                return 1;
            }
            if (this.Text == matchAnswer.Text && this.Partner?.Text == matchAnswer.Partner?.Text)
                return 0;
            return this.Text.CompareTo(matchAnswer.Text);
        }

        public static bool operator ==(MatchAnswer left, MatchAnswer right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MatchAnswer)obj);
        }
        protected bool Equals(MatchAnswer other)
        {
            return this.Text == other.Text && this.Partner?.Text == other.Partner?.Text;
        }

        public static bool operator !=(MatchAnswer left, MatchAnswer right)
        {
            return !(left == right);
        }


        public override Answer GetClearAnswer()
        {
            return new MatchAnswer() { Id = Id, IsCorrect = false, TaskId = TaskId, Text = Text, Side = Side, Task = null, Partner = null!, PartnerId = 0 };
        }
    }
}
