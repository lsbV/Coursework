using System.Drawing;
using TestLib.Interfaces;

namespace TestLib.Classes.Answers
{
    public class ImageAnswer : IAnswer
    {
        private string imageName;
        public bool IsCorrect { get; set; }
        public string Text { get => imageName; set => imageName = value; }
        public Image Image { get; set; }

        public ImageAnswer(bool isCorrect, string text, Image image)
        {
            IsCorrect = isCorrect;
            Text = text;
            Image = image;
        }

        public bool Equals(IAnswer answer)
        {
            if (answer is ImageAnswer imageAnswer)
            {
                return Text == imageAnswer.Text && Image.Equals(imageAnswer.imageName);
            }
            else
            {
                return false;
            }
        }
    }
}
