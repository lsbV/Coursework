using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TestLib.Interfaces
{
    public interface IUserTest
    {
        public int Id { get; set; }
        public IUser User { get; set; }
        public ITest Test { get; set; }
        public bool Passed { get; set; }
        public DateTime PassageDate { get; set; }

    }
}
