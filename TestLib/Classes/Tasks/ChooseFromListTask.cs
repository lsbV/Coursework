using System.Collections.ObjectModel;
using TestLib.Abstractions;
using TestLib.Classes.Bodies;
using TestLib.Interfaces;
using Task = TestLib.Abstractions.Task;

namespace TestLib.Classes.Tasks
{
    public class ChooseFromListTask : Task
    {
        public string Description { get; set; }
        public double Point { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public TaskBody Body { get; set; }

        public ChooseFromListTask(string description, double point, ICollection<Answer> answers, TaskBody body)
        {
            Description = "Choose from list";
            Point = point;
            Answers = new ObservableCollection<Answer>(answers.ToList());
            Body = body;
        }
        public ChooseFromListTask()
        {
            Description = "Choose from list";
            Point = 0;
            Answers = new ObservableCollection<Answer>();
            Body = new TextTaskBody();
        }

        public object Clone()
        {
            var answers = new ObservableCollection<Answer>();
            foreach (var answer in Answers)
            {
                answers.Add((Answer)answer.Clone());
            }
            return new ChooseFromListTask(Description, Point, answers, (TaskBody)Body.Clone());
        }

        public override bool CheckAnswers(ICollection<Answer> answers)
        {
            throw new NotImplementedException();
        }
    }
}
