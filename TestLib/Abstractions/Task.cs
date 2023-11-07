using TestLib.Classes.Bodies;

namespace TestLib.Abstractions
{
    public abstract class Task
    {
        protected Task(int id, string description, TaskBody body, double point, ICollection<Answer> answers)
        {
            Id = id;
            Description = description;
            Body = body;
            Point = point;
            Answers = answers;
        }
        protected Task()
        {
            Id = 0;
            Description = string.Empty;
            Body = new TextTaskBody();
            Point = 0;
            Answers = new List<Answer>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public TaskBody Body { get; set; }
        public double Point { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public abstract bool CheckAnswers(ICollection<Answer> answers);
    }
}