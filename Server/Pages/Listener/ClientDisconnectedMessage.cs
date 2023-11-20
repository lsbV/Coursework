namespace Server.Pages.Listener
{
    internal class ClientDisconnectedMessage
    {
        public string WorkerId { get; }

        public ClientDisconnectedMessage(string workerId)
        {
            this.WorkerId = workerId;
        }
    }
}