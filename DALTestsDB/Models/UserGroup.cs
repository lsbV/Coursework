﻿using TestLib;

namespace DALTestsDB.Model
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public User User { get; set; } = default!;
        public Group Group { get; set; } = default!;
    }
}
