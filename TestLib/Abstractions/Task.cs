using TestLib.Classes.Bodies;
using TestLib.Classes.Tasks;
using TestLib.Classes.Test;

namespace TestLib.Abstractions
{
    public abstract class Task
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Point { get; set; }

        public int TestId { get; set; }
        public int BodyId { get; set; }
        public Test Test { get; set; } = null!;
        public Body Body { get; set; } = null!;
        public List<Answer> Answers { get; set; } = null!;

        public abstract double GetGrade(List<Answer> userAnswers);
        public abstract Task GetClearTask();
        protected TTask TaskClone<TTask>() where TTask : Task, new()
        {
            return new TTask() { Id = Id, Answers = Answers, Body = null!, BodyId = BodyId, Description = Description, Point = Point, Test = null!, TestId = TestId };
        }
        protected TTask ClearTaskClone<TTask>() where TTask : Task, new()
        {
            return new TTask() { Id = Id, Answers = Answers.Select(x => x.GetClearAnswer()).ToList(), Body = (Body)Body.Clone(), BodyId = BodyId, Description = Description, Point = Point, Test = null!, TestId = TestId };
        }
    }
}