using TestLib.Abstractions;
using Task = TestLib.Abstractions.Task;

namespace DALTestsDB
{
    public class UserTask
    {
        public int Id { get; set; }
        public UserTest UserTest { get; set; }
        public Task Task { get; set; }
        public bool IsMissed { get; set; }
        public double? TaskGrade { get; set; }
        public List<UserAnswer> UserAnswers { get; set; }
    }
}
