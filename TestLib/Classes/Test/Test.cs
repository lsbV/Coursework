using TestLib.Abstractions;
using Task = TestLib.Abstractions.Task;

namespace TestLib.Classes.Test
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string InfoForTestTaker { get; set; } = string.Empty;
        public int? CountOfTasks { get => Tasks?.Count; }
        public double PassingPercent { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }


        public double? TaskPointsSum => Tasks?.Sum(s => s.Point);
        public virtual List<Task> Tasks { get; set; } = default!;
        
        public Test GetClearTest()
        {
            return new Test()
            {
                Id = Id,
                Title = Title,
                Author = Author,
                Description = Description,
                InfoForTestTaker = InfoForTestTaker,
                PassingPercent = PassingPercent,
                IsArchived = false,
                CreatedAt = DateTime.MinValue,
                Tasks = Tasks.Select(s => s.GetClearTask()).ToList()
            };
        }
    }
}