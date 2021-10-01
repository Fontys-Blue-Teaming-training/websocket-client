using System;
using System.Collections.Generic;
using System.Text;

namespace websocket_client
{
    public interface IMessageHandler
    {
        public void HandleMessage(string message);
    }
}
