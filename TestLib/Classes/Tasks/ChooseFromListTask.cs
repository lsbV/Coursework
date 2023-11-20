using System.Collections.ObjectModel;
using TestLib.Abstractions;
using TestLib.Classes.Bodies;
using Task = TestLib.Abstractions.Task;

namespace TestLib.Classes.Tasks
{
    public class ChooseFromListTask : Task
    {
        public ChooseFromListTask(string description, double point, List<Answer> answers, Body body)
        {
            Description = "Choose from list";
            Point = point;
            Answers = answers;
            Body = body;
        }
        public ChooseFromListTask()
        {
            Description = "Choose from list";
            Point = 0;
        }

        public object Clone()
        {
            var answers = new List<Answer>(Answers.Select(a=>(Answer)a.Clone()));
            return new ChooseFromListTask() { Id = Id, Answers = answers, Body = (Body)Body.Clone(), BodyId = BodyId, Description = Description, Point = Point, Test = null!, TestId = TestId};
        }

        
        public override double GetGrade(List<Answer> answers)
        {
            //return Point;
            if (answers == null)
            {
                throw new ArgumentNullException(nameof(answers));
            }
            if (answers.Count() != 1)
            {
                throw new ArgumentException("ChooseFromListTask can have only one answer");
            }
            if (Answers.Single(a => a.IsCorrect).Text == answers.Single().Text)
            {
                return Point;
            }
            return 0;
        }

        public override Task GetClearTask()
        {
            var newTask = (ChooseFromListTask)Clone();
            newTask.Answers = new(newTask.Answers.Select(a => a.GetClearAnswer()));
            return newTask;
        }
    }
}
