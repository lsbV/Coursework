using TestLib.Abstractions;
using Task = TestLib.Abstractions.Task;

namespace DALTestsDB.Model
{
    public class UserTaskResult
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserTestResultId { get; set; }


        public Task Task { get; set; } = default!;
        public UserTestResult UserTestResult { get; set; } = default!;
        public List<UserAnswerResult> UserAnswerResults { get; set; } = default!;
        
        
        public double GetTaskGrade()
        {
            var answers = UserAnswerResults.Select(x => x.Answer).ToList();
            return Task.GetGrade(answers);
        } 
    }
}
