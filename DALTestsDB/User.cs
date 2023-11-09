using TestLib.Interfaces;

namespace DALTestsDB
{
    public class User 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Description { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<UserGroup> UserGroups { get; set; }
        public List<Group> Groups { get; set; }
        public List<TestAssignedUser> TestAssignedUsers { get; set; }
        public List<TestAssigned> TestAssigneds { get; set; }
    }
}
