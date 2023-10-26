using TestLib.Interfaces;

namespace TestLib.Classes.Answers
{
    public class TextAnswer : IAnswer
    {
        public bool IsCorrect { get; set; }
        public string Text { get; set; }

        public TextAnswer(bool isCorrect, string text)
        {
            IsCorrect = isCorrect;
            Text = text;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
