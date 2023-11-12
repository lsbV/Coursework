using System.Drawing;
using TestLib.Abstractions;

namespace TestLib.Classes.Answers
{
    public class ImageAnswer : Answer
    {
        public Image Image { get; set; }

        public ImageAnswer(bool isCorrect, string text, Image image)
        {
            IsCorrect = isCorrect;
            Text = text;
            Image = image;
        }

        public override object Clone()
        {
            return new ImageAnswer(IsCorrect, Text, (Image)Image.Clone());
        }

        public override int CompareTo(Answer? other)
        {
            throw new NotImplementedException();
        }
    }
}
