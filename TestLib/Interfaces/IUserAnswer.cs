using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Interfaces
{
    public interface IUserAnswer
    {
        public int Id { get; set; }
        public IUserTest UserTest { get; set; }
        public IAnswer Answer { get; set; }
    }
}
