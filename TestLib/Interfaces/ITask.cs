namespace TestLib.Interfaces
{
    public interface ITask : ICloneable
    {        
        public string Description { get; set; }
        public ITaskBody Body { get; set; }
        public double Point { get; set; }
        public ICollection<IAnswer> Answers { get; set; }
        public bool CheckAnswers(ICollection<IAnswer> answers);
    }
}