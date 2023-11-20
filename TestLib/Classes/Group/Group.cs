﻿
namespace TestLib
{
    public class Group 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set;}
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<User> Users { get; set; } = default!;
    }
}
