using System.Drawing;
using TestLib.Abstractions;
using TestLib.Classes.Enums;

namespace TestLib.Classes.Bodies
{
    public class ImageBody : Body
    {
        public required string ImagePath { get; set; } = string.Empty;
        public required int ImageLength { get; set; }
        public override object Clone()
        {
            return new ImageBody() { Id = Id, ImagePath = ImagePath, ImageLength =  ImageLength, TaskId = TaskId, Text = Text, Task = null! };
        }
    }
}
