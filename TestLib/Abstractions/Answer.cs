namespace TestLib.Abstractions
{
    public abstract class Answer : ICloneable, IComparable<Answer>
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; } = string.Empty;
        public int TaskId { get; set; }
        public Task Task { get; set; } = null!;
        public abstract object Clone();

        public abstract int CompareTo(Answer? other);
    }
}