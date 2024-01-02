
using TestLib.Abstractions;

namespace TestLib.Classes.Tasks
{
    public class MultipleSelectTask : Abstractions.Task
    {
        public override Abstractions.Task GetClearTask()
        {
            return ClearTaskClone<MultipleSelectTask>();
        }

        public override double GetGrade(List<Answer> userAnswers)
        {
            if (userAnswers is null)
            {
                throw new ArgumentNullException(nameof(userAnswers));
            }
            if (userAnswers.Count != Answers.Count)
            {
                throw new ArgumentException("Incorrect count of answers");
            }
            var correctAnswers = Answers.Where(x => x.IsCorrect).ToList();
            var userCorrectAnswers = userAnswers.Where(x => x.IsCorrect).ToList();
            double grade = 0.0;
            double interim = Math.Round(Point / correctAnswers.Count, 3);
            if (userCorrectAnswers.Count > correctAnswers.Count) return 0;

            foreach (var answer in correctAnswers)
            {
                if (userCorrectAnswers.Exists(a => a.CompareTo(answer) == 0))
                {
                    grade += interim;
                }
            }
            return Math.Round(grade, 2);
        }
    }
}
