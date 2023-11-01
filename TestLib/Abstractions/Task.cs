namespace TestLib.Abstractions
{
    public abstract class Task
    {        
        public int Id { get; set; }
        public string Description { get; set; }
        public TaskBody Body { get; set; }
        public double Point { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public abstract bool CheckAnswers(ICollection<Answer> answers);
    }
}