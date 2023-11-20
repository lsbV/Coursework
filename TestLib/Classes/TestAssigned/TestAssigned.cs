using TestLib.Classes.Enums;
using TestLib.Classes.Test;

namespace TestLib
{
    public class TestAssigned
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public ProgresStatus Status 
        { 
            get
            {
                if (DateTime.Now < StartAt)
                    return ProgresStatus.NotStarted;
                else if (DateTime.Now > EndAt)
                    return ProgresStatus.Finished;
                else
                    return ProgresStatus.InProgress;
            }
        }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public TimeSpan TimeToTake { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsArchived { get; set; }


        public Test Test { get; set; } = default!;
        public List<User> Users { get; set; } = default!;

        public TestAssigned GetClearTestAssigned()
        {
             return new TestAssigned()
             {
                 Id = Id,
                 TestId = TestId,
                 StartAt = StartAt,
                 EndAt = EndAt,
                 TimeToTake = TimeToTake,
                 CreatedAt = CreatedAt,
                 IsArchived = false,
                 Test = Test.GetClearTest(),
                 Users = null!
             };
        }
        //public List<UserTestResult> UserTestResults { get; set; } = default!;
    }
}
