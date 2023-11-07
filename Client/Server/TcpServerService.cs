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

namespace Client.Server
{
    public class TcpServerService : IServerService, IDisposable
    {
        TcpClient server;
        IPEndPoint serverEndPoint;
        public TcpServerService(IPEndPoint serverEndPoint)
        {
            server = new TcpClient(serverEndPoint);
        }
        public async Task ConnectAsync()
        {
            await server.ConnectAsync(serverEndPoint);
        }

        public void Dispose()
        {
            server.Dispose();
        }

        public async Task LogInAsync(string username, string password)
        {
            await server.GetStream().WriteAsync(Encoding.UTF8.GetBytes(username));
        }

        public async Task ReciveMessageAsync(CancellationToken token)
        {
            var stream = server.GetStream();
            StreamReader streamReader = new StreamReader(stream);
            while (true)
            {
                if(token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
                if (stream.DataAvailable)
                {
                    var data = await streamReader.ReadToEndAsync();
                    throw new NotImplementedException();
                    var settings = new JsonSerializerSettings() { Formatting = Formatting.Indented };
                    //ITest test = JsonConvert.DeserializeObject<ITest>(data, settings);
                    //IAnswerParser
                }
                

            }
        }

        public async Task SendCompliteTest()
        {
            throw new NotImplementedException();
        }
    }
}
