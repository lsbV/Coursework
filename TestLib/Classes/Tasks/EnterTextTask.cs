using TestLib.Abstractions;
using Task = TestLib.Abstractions.Task;

namespace TestLib.Classes.Tasks
{
    public class EnterTextTask : Task 
    { 
        
        public override Task GetClearTask()
        {
            var clearAnswers = Answers.Select(x => x.GetClearAnswer()).ToList();
            return new EnterTextTask() { Id = Id, Answers = clearAnswers, Body = (Body)Body.Clone(), BodyId = BodyId, Description = Description, Point = Point, Test = null!, TestId = TestId };
        }

        public override double GetGrade(List<Answer> answers)
        {
            if (answers == null)
            {
                throw new ArgumentNullException(nameof(answers));
            }
            if (answers.Count() != 1)
            {
                throw new ArgumentException("EnterTextTask can have only one answer");
            }
            if (Answers.Single(a => a.IsCorrect).Text == answers.Single().Text)
            {
                return Point;
            }
            return 0;
        }
    }
}
