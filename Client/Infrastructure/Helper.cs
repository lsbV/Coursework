using Client.MVVM_Task._Answer;
using Client.MVVM_Task._Body;
using Client.MVVM_Task._Task;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media;
using TestLib.Abstractions;
using TestLib.Classes.Answers;
using TestLib.Classes.Bodies;
using TestLib.Classes.Exceptions;
using TestLib.Classes.Tasks;

namespace Client.Infrastructure
{
    public static class Helper
    {
        public static BaseBodyVM GetBodyViewModel(this Body body)
        {
            return body switch
            {
                TextBody => new TextBodyVM(body),
                ImageBody => new ImageBodyVM(body),
                _ => throw new InvalidTypeException("Unknown body type"),
            };
        }
        public static BaseAnswerVM GetAnswerViewModel(this Answer answer)
        {
            return answer switch
            {
                TextAnswer => new TextAnswerVM(answer),
                ImageAnswer => new ImageAnswerVM(answer),
                MatchAnswer => new MatchAnswerVM(answer),
                EnterTextAnswer => new EnterTextVM(answer),
                _ => throw new InvalidTypeException("Unknown answer type"),
            };
        }
        public static BaseTaskVM GetTaskViewModel(this Task task)
        {
            return task switch
            {
                ChooseFromListTask => new ChooseFromListVM(task),
                MatchTask => new MatchTaskVM(task),
                EnterTextTask => new EnterTextTaskVM(task),
                MultipleSelectTask => new MultipleSelectVM(task),

                _ => throw new InvalidTypeException("Unknown task type"),
            };
        }
        public static ImageSource? DownloadImage(this IFtpProvider ftpProvider, string imagePath, int imageLength)
        {
            byte[]? imageBytes = ftpProvider.DownloadFile(imagePath, imageLength);
            if (imageBytes == null) return null;
            return imageBytes.ToImageSource();
        }

        public static ImageSource? ToImageSource(this byte[] imageBytes)
        {
            try
            {
                var imageSourceConverter = new ImageSourceConverter();
                MemoryStream ms = new MemoryStream(imageBytes);
                return (ImageSource)imageSourceConverter.ConvertFrom(ms)!;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
