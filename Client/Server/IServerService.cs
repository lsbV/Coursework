using System.Threading;
using System.Threading.Tasks;

namespace Client.Server
{
    public interface IServerService
    {
        Task LogInAsync(string username, string password);
        Task ReciveMessageAsync(CancellationToken token);
        Task SendCompliteTest();
        Task ConnectAsync();
    }
}