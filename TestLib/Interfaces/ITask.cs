namespace TestLib.Interfaces
{
    public interface ITask
    {        
        public string Description { get; set; }
        public ITaskBody Body { get; set; }
        public double Point { get; set; }
        public ICollection<IAnswer> Answers { get; set; }
        public ICollection<IAnswer> CorrectAnswers { get; set; }
        public bool CheckAnswers(ICollection<IAnswer> answers);
    }
}