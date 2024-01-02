using System.Drawing;
using TestLib.Abstractions;
using TestLib.Classes.Enums;

namespace TestLib.Classes.Answers
{
    public class ImageAnswer : Answer
    {
        public required string ImagePath { get; set; }
        public required int ImageLength { get; set; }

        public override int CompareTo(Answer? other)
        {
            if(other == null)
            {
                return 1;
            }
            if (other is ImageAnswer imageAnswer)
            {
                return this.CompareTo(imageAnswer);
            }
            return this.Text.CompareTo(other.Text);
        }
        public int CompareTo(ImageAnswer? other)
        {
            if (other == null)
            {
                return 1;
            }
            if (this.ImageLength == other.ImageLength && this.ImagePath == other.ImagePath)
                return 0;
            return this.ImagePath.CompareTo(other.ImagePath);
        }

        public override Answer GetClearAnswer()
        {
            return new ImageAnswer() { Id = Id, IsCorrect = false, TaskId = TaskId, Text = Text, Task = null, ImageLength = ImageLength, ImagePath = ImagePath };
        }
    }
}
