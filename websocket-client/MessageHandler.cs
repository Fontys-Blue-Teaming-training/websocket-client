using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace websocket_client
{
    public abstract class MessageHandler<T> : IMessageHandler
    {
        public static T ReceiveAsObj(string data)
        {
            var a = JsonConvert.DeserializeObject<T>(data);
            Console.WriteLine($"{a}");
            return a;
        }

        public abstract void HandleMessage(string message);
    }
}
