using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Interfaces;

namespace TestLib.Classes.Tasks
{
    public class EnterTextTask : ITask
    {
        public string Description { get; set; }
        public ITaskBody Body { get; set; }
        public double Point { get; set; }
        public ICollection<IAnswer> Answers { get; set; }

        public bool CheckAnswers(ICollection<IAnswer> answers)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
