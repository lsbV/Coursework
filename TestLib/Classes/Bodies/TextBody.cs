using TestLib.Abstractions;

namespace TestLib.Classes.Bodies
{
    public class TextBody : Body
    {
        public TextBody(string text)
        {
            Text = text;
        }
        public TextBody()
        {
            Text = string.Empty;
        }
        public override object Clone()
        {
            return new TextBody() { Text = Text, Id = Id, TaskId = TaskId, Task = null! };
        }
    }
}
