using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Interfaces;

namespace TestLib.Classes.Test
{
    public class Test : ITest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string InfoForTestTaker { get; set; }
        public ICollection<ITask> Tasks { get; set; }
        public double MaxPoints { get; set; }
        public double MinPoints { get; set; }
    }
}
