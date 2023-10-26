namespace TestLib.Interfaces
{
    public interface IAnswer : ICloneable
    {
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
    }
}