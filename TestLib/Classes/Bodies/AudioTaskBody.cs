using TestLib.Abstractions;

namespace TestLib.Classes.Bodies
{
    public class AudioTaskBody : Body
    {
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
