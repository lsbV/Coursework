using System.Threading;

namespace Client.Infrastructure
{
    public class ServerStartedRecivingMessage
    {
        public CancellationToken CancellationToken { get; set; }
    }
}