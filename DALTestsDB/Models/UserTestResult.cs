namespace DALTestsDB.Model
{
    public class UserTestResult
    {
        public int Id { get; set; }
        public int TestAssignedUserId { get; set; }
        public DateTime PassageDate { get; set; }

        //public TestResultStatus GetStatus()
        //{
        //    if (IsMissed) return TestResultStatus.NotPassed;
        //    var passingPercent = TestAssignedUser?.TestAssigned?.Test?.PassingPercent;
        //    if (passingPercent.HasValue == false) return TestResultStatus.Unknown;
        //    if (Result > passingPercent.Value) return TestResultStatus.Passed;
        //    return TestResultStatus.Failed;
        //}


        public TestAssignedUser TestAssignedUser { get; set; } = default!;
        public List<UserTaskResult> UserTaskResults { get; set; } = default!;
        
        
        public double GetTestGrade()
        {
            var res = UserTaskResults.Sum(t => t.GetTaskGrade());
            return res;
        } 
    }
}
