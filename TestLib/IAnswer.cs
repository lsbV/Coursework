namespace TestLib
{
    public interface IAnswer
    {
        public bool IsCorrect { get; set; }
        public abstract string Text { get; set; }
    }
}