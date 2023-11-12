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
        public int TestAssignedId { get; set; }
        public int UserId { get; set; }


        public TestAssigned TestAssigned { get; set; } = default!;
        public User User { get; set; } = default!;
    }
}
