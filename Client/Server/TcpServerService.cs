using Client.Infrastructure;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Interop;
using TestLib.Abstractions;
using TestLib.Classes.Network;
using Task = System.Threading.Tasks.Task;

namespace Client.Server
{
    public class TcpServerService : IServerService
    {
        TcpClient? server;
        ISerializer serializer;
        IMessenger messenger;

        public bool IsConnected => server?.Connected ?? false;

        public TcpServerService(ISerializer serializer, IMessenger messenger)
        {
            this.serializer = serializer;
            this.messenger = messenger;
        }


        public async Task ConnectAsync(IPEndPoint serverEndPoint, CancellationToken token)
        {
            try
            {
                if (server != null)
                {
                    server.Close();
                    server.Dispose();
                }
                server = new TcpClient();
                await server.ConnectAsync(serverEndPoint, token);
            }
            catch(Exception)
            {
                server?.Close();
                server?.Dispose();
                server = null;
                throw;
            }
        }

        public void Dispose()
        {
            if (server != null)
            {
                server.Close();
                server.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        public async Task LogInAsync(string username, string password)
        {
            await SendMessageAsync(new Message() { Header = RequestMessage.LOG_IN, Body = $"{username} {password}" });
        }

        public async Task<Message?> ReciveMessageAsync(CancellationToken token)
        {
            if(server == null)
                throw new Exception("Server is not connected!");
            messenger.Send(new ServerStartedRecivingMessage() { CancellationToken = token });
            Message? message = null;

            var stream = server.GetStream();
            const byte maxMessageLength = 16;
            StringBuilder sb = new StringBuilder();
           
                var messageLength = new byte[maxMessageLength];
                await stream.ReadAsync(messageLength, 0, messageLength.Length);
                if (int.TryParse(Encoding.UTF8.GetString(messageLength), out int bufferSize) == false)
                {
                    return null;
                }
                var buffer = new byte[bufferSize];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                sb.Append(Encoding.UTF8.GetString(buffer));
           


            if (sb.Length == 0)
                return null;
            message = serializer.Deserialize<Message>(sb.ToString());
            messenger.Send(new ServerEndedRecivingMessage() { Message = message });
            return message;
        }


        public async Task<bool> SendMessageAsync(Message message)
        {
            if (server == null)
                throw new Exception("Server is not connected!");
            try
            {
                messenger.Send(new ServerStartedSendingMessage() { Message = message });
                var msg = serializer.Serialize(message);
                StreamWriter writer = new StreamWriter(server.GetStream());
                await writer.WriteAsync(msg.Length.ToString());
                await writer.FlushAsync();
                await writer.WriteAsync(msg);
                await writer.FlushAsync();
                messenger.Send(new ServerEndedSendingMessage() { Message = message });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
