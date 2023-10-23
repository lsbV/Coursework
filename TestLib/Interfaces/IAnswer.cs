namespace TestLib.Interfaces
{
    public interface IAnswer
    {
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
        public bool Equals(IAnswer answer);
    }
}