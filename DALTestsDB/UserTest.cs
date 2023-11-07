namespace DALTestsDB
{
    public class UserTest
    {
        public int Id { get; set; }
        public TestAssignedUser TestAssignedUser { get; set; }
        public TestResultStatus Status { get; set; }
        public bool IsMissed { get; set; }
        public DateTime PassageDate { get; set; }
        public double? Result { get; set; }
        public List<UserTask> UserTasks { get; set; }
    }
}
