using TestLib;

namespace DALTestsDB.Model
{
    public class TestAssignedUser
    {
        public int Id { get; set; }
        public int TestAssignedId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime AppointmentDate { get; set; }


        public TestAssigned TestAssigned { get; set; } = default!;
        public User User { get; set; } = default!;
    }
}
