using DALTestsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;
using TestLib.Classes.Enums;

namespace Server.Infrastructure
{
    public static class Helper
    {
        public static ProgresStatus GetStatus(this DateTime StartAt, DateTime EndAt)
        {
            if (DateTime.Now < StartAt)
                return ProgresStatus.NotStarted;
            else if (DateTime.Now > EndAt)
                return ProgresStatus.Finished;
            else
                return ProgresStatus.InProgress;
        }
        public static Answer GetNewUserAnswer(this Answer answer)
        {
            var newAnswer = (Answer)answer.Clone();
            newAnswer.Id = 0;
            answer.Task = null;
            newAnswer.TaskId = null;
            return newAnswer;
        }
    }
}
