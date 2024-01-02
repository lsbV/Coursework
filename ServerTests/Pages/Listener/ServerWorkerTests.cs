using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;
using TestLib.Classes.Network;
using Task = System.Threading.Tasks.Task;

namespace Server.Pages.Listener.Tests
{
    [TestClass()]
    public class ServerWorkerTests
    {

        [TestMethod()]
        public void ReceiveMessageTest()
        {
            // Arrange

            TcpListener listener = new(IPAddress.Any, 55598);
            IPEndPoint localEndPoint = new(IPAddress.Loopback, (listener.LocalEndpoint as IPEndPoint)!.Port);

            listener.Start();
            using var sender = new TcpClient();
            sender.Connect(localEndPoint);
            TcpClient tcpClient = listener.AcceptTcpClient();
            listener.Stop();

            ILogger logger = Mock.Of<ILogger>();
            ISerializer serializer = new JsonSerializer();
            IEncryptor encryptor = Mock.Of<IEncryptor>();

            using var server = new ServerWorker(tcpClient, serializer, logger, encryptor);
            var message = new Message() { Header = "Test", Body = "Test" };

            // Act
            using var stream = sender.GetStream();


            Message? receivedMessage = null;
            Task t = Task.Run(() =>
            {
                CancellationToken token = new();
                receivedMessage = server.ReceiveMessage(token).Result;
            });
            var bytes = Encoding.UTF8.GetBytes(serializer.Serialize(message));
            stream.Write(Encoding.UTF8.GetBytes(bytes.Length.ToString()));
            stream.Write(bytes, 0, bytes.Length);

            t.Wait();


            // Assert
            Assert.IsNotNull(receivedMessage);
            Assert.AreEqual(message.Header, receivedMessage.Header);
            Assert.AreEqual(message.Body, receivedMessage.Body);
        }

        [TestMethod()]
        public void SendMessageTest()
        {
            // Arrange

            TcpListener listener = new(IPAddress.Any, 55599);
            IPEndPoint localEndPoint = new(IPAddress.Loopback, (listener.LocalEndpoint as IPEndPoint)!.Port);

            listener.Start();
            var sender = new TcpClient();
            sender.Connect(localEndPoint);
            TcpClient tcpClient = listener.AcceptTcpClient();
            listener.Stop();

            ILogger logger = Mock.Of<ILogger>();
            ISerializer serializer = new JsonSerializer();
            IEncryptor encryptor = Mock.Of<IEncryptor>();

            using var server = new ServerWorker(tcpClient, serializer, logger, encryptor);
            using var client = new ServerWorker(sender, serializer, logger, encryptor);
            var message = new Message() { Header = "Test", Body = "Test" };

            // Act
            Message? receivedMessage = null;

            Task t1 = Task.Run(() =>
            {
                server.SendMessage(message);
            });
            Task t2 = Task.Run(() =>
            {
                CancellationToken token = new();
                receivedMessage = client.ReceiveMessage(token).Result;
            });
            Task.WaitAll(t1, t2);

            // Assert
            Assert.IsNotNull(receivedMessage);
            Assert.AreEqual(message.Header, receivedMessage.Header);
            Assert.AreEqual(message.Body, receivedMessage.Body);
        }
    }
}