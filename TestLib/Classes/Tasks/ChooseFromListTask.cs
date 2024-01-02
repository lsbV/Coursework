using System.Collections.ObjectModel;
using TestLib.Abstractions;
using TestLib.Classes.Bodies;
using Task = TestLib.Abstractions.Task;

namespace TestLib.Classes.Tasks
{
    public class ChooseFromListTask : Task
    {
        public ChooseFromListTask(string? description, double point, List<Answer> answers, Body body)
        {
            Description = description ?? "Choose from list";
            Point = point;
            Answers = answers;
            Body = body;
        }
        public ChooseFromListTask()
        {
            Description = "Choose from list";
            Point = 0;
        }

        /// <summary>
        /// Get grade for this task
        /// </summary>
        /// <param name="userAnswers"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public override double GetGrade(List<Answer> userAnswers)
        {
            if (userAnswers == null)
            {
                throw new ArgumentNullException(nameof(userAnswers));
            }
            if (userAnswers.Count != 1)
            {
                if (userAnswers.Count == 0)
                {
                    return 0;
                }
                throw new ArgumentException("ChooseFromListTask can have only one answer");
            }
            if (Answers.Single(a => a.IsCorrect).Text == userAnswers.Single().Text)
            {
                return Point;
            }
            return 0;
        }

        public override Task GetClearTask()
        {
            var newTask = new ChooseFromListTask
            {
                Id = Id,
                Body = (Body)Body.Clone(),
                BodyId = BodyId,
                Description = Description,
                Point = Point,
                Test = null!,
                TestId = TestId,
                Answers = new(this.Answers.Select(a => a.GetClearAnswer()))
            };
            return newTask;
        }
    }
}
