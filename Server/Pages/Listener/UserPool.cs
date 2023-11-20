using CommunityToolkit.Mvvm.Messaging;
using Server.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestLib;

namespace Server.Pages.Listener
{
    public class UserPool : IDisposable
    {
        private static readonly object locker = new object();
        private static readonly IMessenger messenger = DI.Create<IMessenger>();

        private Dictionary<string, ServerUser> pool;
        public static UserPool Instance { get; } = new UserPool();
        protected UserPool()
        {
            pool = new Dictionary<string, ServerUser>();
        }

        public void Add(string id, ServerUser client)
        {
            lock (locker)
                pool.Add(id, client);
            messenger.Send(new ClientConnectedMessage(client));
        }

        public void Remove(string id)
        {
            lock (locker)
                pool.Remove(id);
            messenger.Send(new ClientDisconnectedMessage(id));
        }

        public ServerUser? GetByWorkerId(string id)
        {
            lock (locker)
            {
                if (pool.ContainsKey(id))
                {
                    return pool[id];
                }
                return null;
            }
            
        }
        public ServerUser? GetByUserId(int userId)
        {
            lock (locker)
            {
                return pool.Values.FirstOrDefault(x => x.UserId == userId);
            }
        }

        public bool Contains(string id)
        {
            return pool.ContainsKey(id);
        }

        public void Dispose()
        {
            lock (locker)
            {
                foreach (var item in pool)
                {
                    item.Value.ServerWorker.Dispose();
                }
                pool.Clear();
            }
            GC.SuppressFinalize(this);
        }
    }
    public class ServerUser
    {
        public string WorkerId => ServerWorker.Id;
        public int UserId { get; set; } = -1;
        public string Login { get; set; } = string.Empty;
        public ServerWorker ServerWorker { get; }
        public CancellationTokenSource CancellationSource { get; }   
        public ServerUser(ServerWorker serverWorker, CancellationTokenSource token)
        {
            ServerWorker = serverWorker;
            CancellationSource = token;
        }
    }
}
