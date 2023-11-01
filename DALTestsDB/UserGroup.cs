﻿using DALTestsDB;

namespace TestLib.Interfaces
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public User User { get; set; }
        public Group Group { get; set; }
    }
}
