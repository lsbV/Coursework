namespace TestLib.Abstractions
{
    public abstract class Answer : ICloneable
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; } = null!;
        public abstract object Clone();
    }
}