namespace TestLib.Abstractions
{
    public abstract class Answer : ICloneable
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }

        public abstract object Clone();
    }
}