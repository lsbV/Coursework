namespace TestLib.Abstractions
{
    public abstract class Body : ICloneable
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int TaskId { get; set; }


        public Task Task { get; set; } = null!; 
        public abstract object Clone();
        protected TBody BodyClone<TBody>() where TBody : Body, new()
        {
            return new TBody() { Id = Id, TaskId = TaskId, Text = Text, Task = null };
        }
    }
}
