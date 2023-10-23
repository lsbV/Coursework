namespace TestLib
{
    public interface ITest
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string InfoForTestTaker { get; set; }
        public List<ITask> Tasks { get; set; }
        public int CountOfTasks { get => Tasks.Count; }
        public double MaxPoints { get; set; }
        public double MinPoints { get; set; }
    }
}