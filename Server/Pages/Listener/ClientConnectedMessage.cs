namespace Server.Pages.Listener
{
    internal class ClientConnectedMessage
    {
        public ServerUser Client { get; }

        public ClientConnectedMessage(ServerUser client)
        {
            this.Client = client;
        }
    }
}