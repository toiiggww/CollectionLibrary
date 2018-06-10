using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace TEArts.Etc.CollectionLibrary
{
    public static class SocketLinger
    {
        public static readonly LingerOption Linger = new LingerOption(true, 0);
        public static void ResetConnection(this Socket socket)
        {
            if (socket != null) { socket.LingerState = Linger; }
        }
    }
}
