using TestLib.Abstractions;

namespace DALTestsDB.Model
{
    public class UserAnswerResult
    {
        public int Id { get; set; }
        public int UserTaskResultId { get; set; }
        public int AnswerId { get; set; }


        public UserTaskResult UserTaskResult { get; set; } = default!;
        public Answer Answer { get; set; } = default!;
    }
}
