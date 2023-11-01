using TestLib.Abstractions;

namespace TestLib.Classes.Bodies
{
    public class TextTaskBody : TaskBody
    {
        public TextTaskBody(string? text)
        {
            Text = text;
        }
        public TextTaskBody()
        {
            Text = string.Empty;
        }
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
