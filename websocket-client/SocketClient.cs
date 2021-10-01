using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatsonWebsocket;


namespace websocket_client
{
    public class SocketClient
    {
        WatsonWsClient client = new WatsonWsClient("145.93.58.39", 3002, false);  //client connects to the server, parameters are the servers ip and port.
        private IMessageHandler handler;


        public SocketClient(IMessageHandler messageHandler)
        {
            handler = messageHandler;
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
            await client.SendAsync(JsonConvert.SerializeObject(message));  //client only needs to send message to the server, no ip is needed since there's only 1 server to connect to.
        }

        public void MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            var action = Encoding.UTF8.GetString(args.Data);
            handler.HandleMessage(action);
            Task.Run(async () => MessageHandler<ScenarioMessage>.ReceiveAsObj(action));
        }

        public void ServerConnected(object sender, EventArgs args)
        {
            Console.WriteLine("Server connected");
        }

        public void ServerDisconnected(object sender, EventArgs args)
        {
            Console.WriteLine("Server connected");

        }



    }
}
