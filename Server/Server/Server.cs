using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.IO;
using Client;

namespace Server
{
    class Server
    {
        public static TcpListener start(System.Net.IPAddress host,int port)
        {
            TcpListener listener = new TcpListener(host, port);
            listener.Start();
            return listener;
        }

        public static void SendToClient(TcpClient client, List<SomeData> data)
        {
            BinaryWriter writer = new BinaryWriter(client.GetStream());
            writer.Write(data.Count);
            foreach (SomeData d in data)
            {

                byte[] b = Helper.Serilize(d);
                writer.Write(b.Length);
                writer.Write(b);
            }

            writer.Flush();
        }
    }
}
