﻿namespace TestLib.Interfaces
{
    public interface ITest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string InfoForTestTaker { get; set; }
        public ICollection<ITask> Tasks { get; set; }
        public int CountOfTasks { get => Tasks.Count; }
        public double MaxPoints { get; set; }
        public double MinPoints { get; set; }
    }
}