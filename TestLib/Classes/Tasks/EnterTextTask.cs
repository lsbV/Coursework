using TestLib.Abstractions;
using Task = TestLib.Abstractions.Task;

namespace TestLib.Classes.Tasks
{
    public class EnterTextTask : Task 
    { 
        public string Description { get; set; }
        public TaskBody Body { get; set; }
        public double Point { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public override bool CheckAnswers(ICollection<Answer> answers)
        {
            throw new NotImplementedException();
        }
    }
}
