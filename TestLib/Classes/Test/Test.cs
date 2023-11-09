using TestLib.Abstractions;
using Task = TestLib.Abstractions.Task;

namespace TestLib.Classes.Test
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string InfoForTestTaker { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public int CountOfTasks { get => Tasks.Count; }
        public double PassingPercent { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}