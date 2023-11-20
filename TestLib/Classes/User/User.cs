namespace TestLib
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

        public List<Group> Groups { get; set; } = default!;
        public List<TestAssigned> TestAssigneds { get; set; } = default!;
    }
}
