using Client.MVVM_Task._Answer;
using Client.MVVM_Task._Body;
using Client.MVVM_Task._Task;
using System;
using System.IO;
using System.Windows.Media;
using TestLib.Abstractions;
using TestLib.Classes.Answers;
using TestLib.Classes.Bodies;
using TestLib.Classes.Tasks;

namespace Client.Infrastructure
{
    public static class Helper
    {
        public static BaseBodyVM GetBodyViewModel(this Body body)
        {
            switch (body)
            {
                case TextBody:
                    return new TextBodyVM(body);
                case ImageBody:
                    return new ImageBodyVM(body);
                default:
                    throw new Exception("Unknown body type");
            }
        }
        public static BaseAnswerVM GetAnswerViewModel(this Answer answer)
        {
            switch (answer)
            {
                case TextAnswer:
                    return new TextAnswerVM(answer);
                default:
                    throw new Exception("Unknown answer type");
            }
        }
        public static BaseTaskVM GetTaskViewModel(this Task task)
        {
            switch (task)
            {
                case ChooseFromListTask:
                    return new ChooseFromListVM(task);
                default:
                    throw new Exception("Unknown task type");
            }
        }
        public static ImageSource? DownloadImage(this IFtpProvider ftpProvider, string imagePath, int imageLength)
        {
            byte[]? imageBytes = ftpProvider.DownloadFile(imagePath, imageLength);
            if (imageBytes == null) return null;
            return imageBytes.ToImageSource();
        }

        public static ImageSource? ToImageSource(this byte[] imageBytes)
        {
            var imageSourceConverter = new ImageSourceConverter();
            MemoryStream ms = new MemoryStream(imageBytes);
            return (ImageSource)imageSourceConverter.ConvertFrom(ms)!;
        }
    }
}
