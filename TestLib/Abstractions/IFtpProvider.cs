using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Abstractions
{
    public interface IFtpProvider
    {
        byte[]? DownloadFile(string remotePath, int fileLength, string login = "user", string password = "user");
        bool UploadFile(string hostAddress, string remotePath, string localPath, string login = "user", string password = "user");
    }
}
