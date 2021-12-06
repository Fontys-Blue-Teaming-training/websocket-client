using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WatsonWebsocket;

namespace websocket_client
{
    public class SocketClient
    {
        private readonly WatsonWsClient client = null;  //client connects to the server, parameters are the servers ip and port.
        private readonly IMessageHandler handler;
        private bool isAutoReconnecting = false;
        private readonly bool autoReconnect;

        public SocketClient(IMessageHandler messageHandler, string ipAddress, bool autoReconnect = true)
        {
            client = new WatsonWsClient(ipAddress, 3002, false);
            handler = messageHandler;
            this.autoReconnect = autoReconnect;
            handler.SetSocketClient(this);
        }

        public void InitClient()
        {
            client.ServerConnected += ServerConnected;
            client.ServerDisconnected += ServerDisconnected;
            client.MessageReceived += MessageReceived;
        }

        public async Task StartClient()
        {
           await client.StartAsync();
        }

        public async Task SendMessage(string message)
        {
            await client.SendAsync(message);  //client only needs to send message to the server, no ip is needed since there's only 1 server to connect to.
        }

        public void MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            var action = Encoding.UTF8.GetString(args.Data);
            handler.HandleMessage(action);
        }

        public void ServerConnected(object sender, EventArgs args)
        {
            Console.WriteLine("Server connected");
        }

        public void ServerDisconnected(object sender, EventArgs args)
        {
            Console.WriteLine("Server diconnected");

            if(autoReconnect && !isAutoReconnecting)
            {
                isAutoReconnecting = true;
                do
                {
                    Console.WriteLine("Trying to auto reconnect...");
                    Task.Run(() => StartClient());
                    Thread.Sleep(1000);
                } while (!client.Connected);
                isAutoReconnecting = false;
            }
        }
    }
}
