using TestLib.Abstractions;
using Task = TestLib.Abstractions.Task;

namespace DALTestsDB
{
    public class UserTaskResult
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserTestResultId { get; set; }
        //public bool IsMissed { get; set; }
        public double TaskGrade => /*IsMissed ? 0 :*/ Task.GetGrade(UserAnswerResults.Select(x=>x.Answer));


        public Task Task { get; set; } = default!;
        public UserTestResult UserTestResult { get; set; } = default!;
        public List<UserAnswerResult> UserAnswerResults { get; set; } = default!;
    }
}
