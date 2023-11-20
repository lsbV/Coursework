using TestLib.Abstractions;

namespace TestLib.Classes.Bodies
{
    public class AudioBody : Body
    {
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
