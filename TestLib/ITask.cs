namespace TestLib
{
    public interface ITask
    {
        
        public string Text { get; set; }
        public double Point { get; set; }
        public IAnswer[] Answers { get; set; }
        public IAnswer[] CorrectAnswers { get; set; }
    }
}