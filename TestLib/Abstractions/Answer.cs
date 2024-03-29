﻿using TestLib.Classes.Answers;

namespace TestLib.Abstractions
{
    public abstract class Answer : IComparable<Answer>
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; } = string.Empty;
        public int? TaskId { get; set; }


        public Task? Task { get; set; } = null!;


        protected TAnswer AnswerClone<TAnswer>() where TAnswer : Answer, new()
        {
            return new TAnswer() { Id = Id, IsCorrect = IsCorrect, TaskId = TaskId, Text = Text, Task = null };
        }
        public virtual Answer ClearDbData()
        {
            Id = 0;
            TaskId = null;
            return this;
        }
        public abstract int CompareTo(Answer? other);
        public abstract Answer GetClearAnswer();

    }
}