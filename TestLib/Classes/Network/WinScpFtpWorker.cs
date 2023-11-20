using TestLib.Abstractions;
using WinSCP;

namespace TestLib.Classes.Network
{
    public class WinScpFtpWorker : IFtpProvider
    {
        public string HostAddress { get; set; }
        public WinScpFtpWorker(string hostAddress)
        {
            HostAddress = hostAddress;
        }
        public bool UploadFile(string hostAddress, string remotePath, string localPath, string login = "user", string password = "user")
        {
            try
            {
                using Session session = new Session();

                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = HostAddress,
                    UserName = login,
                    Password = password,
                };
                session.Open(sessionOptions);

                session.PutFiles(localPath, remotePath).Check();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public byte[]? DownloadFile(string remotePath, int fileLength, string login = "user", string password = "user")
        {
            try
            {
                using Session session = new Session();

                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = HostAddress,
                    UserName = login,
                    Password = password,
                };
                session.Open(sessionOptions);

                byte[] fileContent = new byte[fileLength];
                int partSize = 1_024;
                int left = fileLength;
                using (Stream stream = session.GetFile(remotePath))
                {
                    while (left != 0)
                    {
                        if (partSize > left)
                        {
                            partSize = left;
                        }
                        byte[] buffer = new byte[partSize];
                        var readed = stream.Read(buffer, 0, partSize);
                        buffer.CopyTo(fileContent, fileLength - left);
                        left -= readed;
                    }
                }

                return fileContent;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
