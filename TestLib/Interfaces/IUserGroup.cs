using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Interfaces
{
    public interface IUserGroup
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
    }
}
