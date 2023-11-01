namespace TestLib.Abstractions
{
    public abstract class TaskBody : ICloneable
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public TaskBody(int id, string? text)
        {
            Id = id;
            Text = text;
        }
        public TaskBody()
        {
            Id = 0;
            Text = string.Empty;
        }

        public abstract object Clone();
    }
}
