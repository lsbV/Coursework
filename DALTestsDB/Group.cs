using TestLib.Interfaces;

namespace DALTestsDB
{
    public class Group 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set;}
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<UserGroup> UserGroups { get; set; }
    }
}
