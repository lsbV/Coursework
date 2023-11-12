using TestLib.Interfaces;

namespace DALTestsDB
{
    public class User 
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }

        //public List<UserGroup> UserGroups { get; set; } = default!;
        public List<Group> Groups { get; set; } = default!;
        //public List<TestAssignedUser> TestAssignedUsers { get; set; } = default!;
        public List<TestAssigned> TestAssigneds { get; set; } = default!;
    }
}
