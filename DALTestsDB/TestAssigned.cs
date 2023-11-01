using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALTestsDB
{
    public class TestAssigned
    {
        public int Id { get; set; }
        public Test Test { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public DateTime StartAt { get; set; }
        public TimeSpan ActiveTime { get; set; }
        public DateTime EndAt { get => StartAt.Add(ActiveTime); }
        public List<TestAssignedUser> UserTests { get; set; }
    }
}
