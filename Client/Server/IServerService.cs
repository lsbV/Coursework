using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TestLib.Classes.Network;

namespace Client.Server
{
    public interface IServerService : IDisposable
    {
        bool IsConnected { get; }
        Task LogInAsync(string username, string password);
        Task<Message?> ReciveMessageAsync(CancellationToken token);
        Task<bool> SendMessageAsync(Message message);
        Task ConnectAsync(IPEndPoint serverEndPoint, CancellationToken token);
    }
}