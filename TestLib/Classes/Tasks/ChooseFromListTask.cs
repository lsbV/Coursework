using System.Collections.ObjectModel;
using TestLib.Classes.Bodies;
using TestLib.Interfaces;

namespace TestLib.Classes.Tasks
{
    public class ChooseFromListTask : ITask
    {
        public string Description { get; set; }
        public double Point { get; set; }
        public ICollection<IAnswer> Answers { get; set; }
        //public ICollection<IAnswer> CorrectAnswers { get; set; }
        public ITaskBody Body { get; set; }

        public ChooseFromListTask(string description, double point, ICollection<IAnswer> answers, ITaskBody body)
        {
            Description = "Choose from list";
            Point = point;
            Answers = new ObservableCollection<IAnswer>(answers.ToList());
            //CorrectAnswers = correctAnswers;
            Body = body;
        }
        public ChooseFromListTask()
        {
            Description = "Choose from list";
            Point = 0;
            Answers = new ObservableCollection<IAnswer>();
            //CorrectAnswers = new ObservableCollection<IAnswer>();
            Body = new TextTaskBody();
        }
        public bool CheckAnswers(ICollection<IAnswer> answers)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            var answers = new ObservableCollection<IAnswer>();
            foreach (var answer in Answers)
            {
                answers.Add((IAnswer)answer.Clone());
            }
            return new ChooseFromListTask(Description, Point, answers, (ITaskBody)Body.Clone());
        }
    }
}
