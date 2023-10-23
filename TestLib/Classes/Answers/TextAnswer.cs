using TestLib.Interfaces;

namespace TestLib.Classes.Answers
{
    public class TextAnswer : IAnswer
    {
        public bool IsCorrect { get; set; }
        public string Text { get; set; }

        public bool Equals(IAnswer answer)
        {
            if (answer is TextAnswer textAnswer)
            {
                return Text == textAnswer.Text;
            }
            else
            {
                return false;
            }
        }
    }
}
