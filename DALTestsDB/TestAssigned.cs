using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Classes.Test;

namespace DALTestsDB
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
        //public TimeSpan ActiveTime => new TimeSpan( EndAt.Ticks - StartAt.Ticks);

        public DateTime CreatedAt { get; set; }
        public bool IsArchived { get; set; }

        public Test Test { get; set; } = default!;
        //public List<TestAssignedUser> TestAssignedUsers { get; set; } = default!;
        public List<User> Users { get; set; } = default!;
    }
}
