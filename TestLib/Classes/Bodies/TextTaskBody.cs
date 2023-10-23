using TestLib.Interfaces;

namespace TestLib.Classes.Bodies
{
    public class TextTaskBody : ITaskBody
    {
        public string? Text { get; set; } = string.Empty;

        public TextTaskBody(string? text)
        {
            Text = text;
        }

        public TextTaskBody()
        {
        }
    }
}
