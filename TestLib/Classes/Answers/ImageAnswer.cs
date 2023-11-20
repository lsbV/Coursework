using System.Drawing;
using TestLib.Abstractions;
using TestLib.Classes.Enums;

namespace TestLib.Classes.Answers
{
    public class ImageAnswer : Answer
    {
        public required string ImagePath { get; set; }
        public required int ImageLength { get; set; }
        public override object Clone()
        {
            return new ImageAnswer() { Id = Id, IsCorrect = IsCorrect, TaskId = TaskId, Text = Text, Task = null, ImageLength = ImageLength, ImagePath = ImagePath };
        }

        public override int CompareTo(Answer? other)
        {
           if(this.Text == other?.Text)
                return 0;
            return 1;
        }

        public override Answer GetClearAnswer()
        {
            return new ImageAnswer() { Id = Id, IsCorrect = false, TaskId = TaskId, Text = Text, Task = null, ImageLength = ImageLength, ImagePath = ImagePath };
        }
    }
}
