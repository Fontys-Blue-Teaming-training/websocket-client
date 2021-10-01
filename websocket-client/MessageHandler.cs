using System;
using Newtonsoft.Json;

namespace websocket_client
{
    public abstract class MessageHandler<T> : IMessageHandler
    {
        public static T ReceiveAsObj(string data)
        {
            var obj = JsonConvert.DeserializeObject<T>(data);
            Console.WriteLine($"{obj}");
            return obj;
        }

        public abstract void HandleMessage(string message);
    }
}
