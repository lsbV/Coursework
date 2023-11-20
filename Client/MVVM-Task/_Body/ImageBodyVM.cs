using Client.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using Server.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TestLib.Abstractions;
using TestLib.Classes.Bodies;

namespace Client.MVVM_Task._Body
{
    public partial class ImageBodyVM : BaseBodyVM
    {
        private IFtpProvider ftpProvider;
        [ObservableProperty] ImageSource? imageSource;
        public ImageBodyVM(Body body) : base(body)
        {
            ftpProvider = DI.Create<IFtpProvider>();
            ImageSource = LoadImage();
        }
        public ImageSource? LoadImage()
        {
            ImageBody imageBody = (ImageBody)body;
            return ftpProvider.DownloadImage(imageBody.ImagePath, imageBody.ImageLength);
        }
    }
}
