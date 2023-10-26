using System.Net.Http.Headers;
using TestLib.Interfaces;

namespace TestLib.Classes.Bodies
{
    public class VideoTaskBody : ITaskBody
    {
        public string? Text { get; set; } = string.Empty;

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
