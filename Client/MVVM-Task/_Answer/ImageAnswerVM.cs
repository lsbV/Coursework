using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;
using TestLib.Classes.Answers;

namespace Client.MVVM_Task._Answer
{
    public class ImageAnswerVM : BaseAnswerVM
    {
        public ImageAnswerVM(Answer answer) : base(answer)
        {
        }

        public override Answer GetAnswerResult()
        {
            ImageAnswer imageAnswer = (ImageAnswer)answer;
            return new ImageAnswer() { Id = 0, IsCorrect = IsCorrect, TaskId = answer.TaskId, Text = Text, ImageLength = imageAnswer.ImageLength, ImagePath = imageAnswer.ImagePath, Task = null };
        }
    }
}
