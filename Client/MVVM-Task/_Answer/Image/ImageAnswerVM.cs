using Client.Infrastructure;
using Server.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TestLib.Abstractions;
using TestLib.Classes.Answers;

namespace Client.MVVM_Task._Answer
{
    public class ImageAnswerVM : BaseAnswerVM
    {
        public ImageSource? ImageSource { get; set; }
        public ImageAnswerVM(Answer answer) : base(answer)
        {
            ImageAnswer imageAnswer = (ImageAnswer)answer;
            IFtpProvider provider = DI.Create<IFtpProvider>();
            ImageSource = provider.DownloadImage(imageAnswer.ImagePath, imageAnswer.ImageLength);
        }

        public override Answer GetAnswerResult()
        {
            ImageAnswer imageAnswer = (ImageAnswer)answer;
            return new ImageAnswer() { Id = 0, IsCorrect = IsCorrect, TaskId = answer.TaskId, Text = Text, ImageLength = imageAnswer.ImageLength, ImagePath = imageAnswer.ImagePath, Task = null };
        }
    }
}
