using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Net.Sockets;

namespace ReceiveFiles
{
    class Server
    {
        public static TcpListener start(System.Net.IPAddress host,int port)
        {
            TcpListener listener = new TcpListener(host, port);
            listener.Start();
            return listener;
        }
    }
}
