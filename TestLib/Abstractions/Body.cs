namespace TestLib.Abstractions
{
    public abstract class Body : ICloneable
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public Body(int id, string? text)
        {
            Id = id;
            Text = text;
        }
        public Body()
        {
            Id = 0;
            Text = string.Empty;
        }

        public abstract object Clone();
    }
}
