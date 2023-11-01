using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALTestsDB
{
    public class TestAssignedUser
    {
        public int Id { get; set; }
        public TestAssigned TestAssigned { get; set; }
        public User User { get; set; }
    }
}
