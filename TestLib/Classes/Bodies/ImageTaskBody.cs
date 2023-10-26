using System.Drawing;
using TestLib.Interfaces;

namespace TestLib.Classes.Bodies
{
    public class ImageTaskBody : ITaskBody
    {
        public string? Text { get; set; }
        public Bitmap Image { get; set; }

        public object Clone()
        {
            return new ImageTaskBody
            {
                Text = Text,
                Image = (Bitmap)Image?.Clone()
            };
        }
    }
}
