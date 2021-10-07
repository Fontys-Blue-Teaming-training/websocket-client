using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace websocket_client
{
    public abstract class MessageHandler<T> : IMessageHandler
    {
        SocketClient client;


        public static T ReceiveAsObj(string data)
        {
            var a = JsonConvert.DeserializeObject<T>(data);
            Console.WriteLine($"{a}");
            return a;
        }

        public void SetSocketClient(SocketClient socketClient)
        {
            client = socketClient;
        }

        public async Task SendMessage(string message)
        {
            await client.SendMessage(message);
        }

        public abstract void HandleMessage(string message);


    }
}
