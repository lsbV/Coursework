using TestLib.Abstractions;
namespace TestLib.Classes.Answers
{
    public class TextAnswer : Answer
    { 
        public TextAnswer(bool isCorrect, string text)
        {
            IsCorrect = isCorrect;
            Text = text;
        }
        public TextAnswer()
        {
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
