using TestLib.Classes.Bodies;
using TestLib.Interfaces;

namespace TestLib.Classes.Tasks
{
    public class ChooseFromListTask : ITask
    {
        public string Description { get; set; }
        public double Point { get; set; }
        public ICollection<IAnswer> Answers { get; set; }
        public ICollection<IAnswer> CorrectAnswers { get; set; }
        public ITaskBody Body { get; set; }

        public ChooseFromListTask(string description, double point, ICollection<IAnswer> answers, ICollection<IAnswer> correctAnswers, ITaskBody body)
        {
            Description = "Choose from list";
            Point = point;
            Answers = answers;
            CorrectAnswers = correctAnswers;
            Body = body;
        }
        public ChooseFromListTask()
        {
            Description = "Choose from list";
            Point = 0;
            Answers = new List<IAnswer>();
            CorrectAnswers = new List<IAnswer>();
            Body = new TextTaskBody();
        }
        public bool CheckAnswers(ICollection<IAnswer> answers)
        {
            throw new NotImplementedException();
        }
    }
}
