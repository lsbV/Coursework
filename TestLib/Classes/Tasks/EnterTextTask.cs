using TestLib.Abstractions;
using Task = TestLib.Abstractions.Task;

namespace TestLib.Classes.Tasks
{
    public class EnterTextTask : Task 
    { 
        public override bool CheckAnswers(ICollection<Answer> answers)
        {
            throw new NotImplementedException();
        }
    }
}
