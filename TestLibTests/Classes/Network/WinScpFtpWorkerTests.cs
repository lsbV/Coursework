using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestLib.Classes.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TestLib.Classes.Network.Tests
{
    [TestClass()]
    public class WinScpFtpWorkerTests
    {
        [TestMethod()]
        public void DownloadFileTest()
        {
            // Arrange
            var ftp = new WinScpFtpWorker(Dns.GetHostName());
            FileInfo fileInfo = new FileInfo("D:\\FTP\\text.txt");

            // Act
            var fileBytes = ftp.DownloadFile(fileInfo.Name, (int)fileInfo.Length);
            using var fs = fileInfo.OpenRead();

            // Assert
            Assert.IsNotNull(fileBytes);

            for (int i = 0; i < fileInfo.Length; i++)
            {
                if (fs.ReadByte() != fileBytes[i])
                    Assert.Fail();
            }

            Assert.IsTrue(true);
        }
    }
}