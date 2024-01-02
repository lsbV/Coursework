using System.Collections.Generic;
using TestLib.Abstractions;
using TestLib.Classes.Answers;
using Task = TestLib.Abstractions.Task;

namespace TestLib.Classes.Tasks
{
    public class MatchTask : Task
    {

        public override Task GetClearTask()
        {
            return ClearTaskClone<MatchTask>();
        }

        public override double GetGrade(List<Answer> userAnswers)
        {
            if (userAnswers == null)
            {
                throw new ArgumentNullException(nameof(userAnswers));
            }
            if (userAnswers.Count > Answers.Count)
            {
                throw new ArgumentException($"Incorrect answers count |TaskID:{Id}|");
            }

            double grade = 0;
            double interim = Math.Round(Point / (Answers.Count / 2), 3); // because each answer has 2 parts
            var leftTruePart = Answers.Cast<MatchAnswer>().Where(a => a.Side == Enums.MatchSide.Left).ToList();
            var leftUserPart = userAnswers.Cast<MatchAnswer>().Where(a => a.Side == Enums.MatchSide.Left).ToList();

            foreach (var userAnswer in leftUserPart)
            {
                foreach (var trueAnswer in leftTruePart)
                {
                    if (userAnswer == trueAnswer)
                    {
                        grade += interim;
                    }
                }
            }

            return Math.Round(grade, 2);
        }
    }
}
