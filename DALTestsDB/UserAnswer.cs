using TestLib.Abstractions;
using TestLib.Interfaces;

namespace DALTestsDB
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public UserTask UserTest { get; set; }
        public Answer Answer { get; set; }
    }
}
