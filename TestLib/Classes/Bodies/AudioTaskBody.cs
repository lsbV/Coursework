using TestLib.Abstractions;

namespace TestLib.Classes.Bodies
{
    public class AudioTaskBody : TaskBody
    {
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
