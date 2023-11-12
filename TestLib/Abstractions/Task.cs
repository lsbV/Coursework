using TestLib.Classes.Bodies;
using TestLib.Classes.Test;

namespace TestLib.Abstractions
{
    public abstract class Task
    {
        protected Task(int id, string description, Body body, double point, ICollection<Answer> answers)
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
            Point = 0;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public double Point { get; set; }

        public int TestId { get; set; }
        public int BodyId { get; set; }
        public Test Test { get; set; } = null!;
        public Body Body { get; set; } = null!;
        public ICollection<Answer> Answers { get; set; } = null!;

        public abstract double GetGrade(IEnumerable<Answer> answers);
        public abstract bool CheckAnswers(ICollection<Answer> answers);
        public abstract Task GetClearTask();
    }
}