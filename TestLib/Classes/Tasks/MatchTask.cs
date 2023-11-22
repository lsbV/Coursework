using System.Collections.Generic;
using TestLib.Abstractions;
using TestLib.Classes.Answers;
using Task = TestLib.Abstractions.Task;
using System.Linq;

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
            if (userAnswers.Count > Answers.Count)
            {
                throw new ArgumentException($"Incorrect answers count |TaskID:{Id}|");
            }
            IEnumerable<MatchAnswer> userAnsvers = userAnswers.Select(a => (MatchAnswer)a);
            IEnumerable<MatchAnswer> trueAnswers = Answers.Select(a => (MatchAnswer)a);

            double grade = 0;
            double interim = Point / Answers.Count;
            var leftTruePart = trueAnswers.Where(a => a.Side == Enums.MatchSide.Left);
            var leftUserPart = userAnsvers.Where(a => a.Side == Enums.MatchSide.Left);
            foreach (var userAnswer in leftUserPart.Where(userAnswer => leftTruePart.Contains(userAnswer)))
            {
                var trueAnswer = leftTruePart.Single(a => a == userAnswer);
                if (trueAnswer.Text == userAnswer.Text && trueAnswer.Partner!.Text == userAnswer.Partner!.Text)
                {
                    grade += interim;
                }
            }

            return grade;
        }
    }
}
