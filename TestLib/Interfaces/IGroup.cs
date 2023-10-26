﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Interfaces
{
    public interface IGroup 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set;}
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
