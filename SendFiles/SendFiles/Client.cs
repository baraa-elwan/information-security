using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Security.Cryptography;
using ReceiveFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Net;
using System.Diagnostics;

namespace SendFiles
{
    class Client
    {
        public TcpClient socket { get; set; }
        
        public string SIP;
        Process currentProcess = Process.GetCurrentProcess();
        public int SPort;
        public Client()
        {
            socket = new TcpClient();
            
        }
        public void connect(string host, int port)
        {
            //connect to server
            socket.Connect(
                System.Net.IPAddress.Parse(host),
                port
                );
            SIP = host;
            SPort = port;

        }

        public void disconnect()
        {
            socket.Close();
        }

        public void sendUserData(string UName, string PublicK)
        {
            
            try
            {
               NetworkStream netstream = socket.GetStream();
                BinaryWriter writer = new BinaryWriter(netstream);
                
                    writer.Write(1);
                    writer.Write(PublicK);
                    writer.Write(UName);
                    writer.Flush();
                
              
            }
            catch
            {

            }
        }
        public List<SomeData> ReceiveClientList()
        {
            sendReq();
            List<SomeData> data = new List<SomeData>();
            //netstream = socket.GetStream();
            BinaryReader reader = new BinaryReader(socket.GetStream());
            
            int len = reader.ReadInt32();
            for (int i = 0; i < len; i++)
            {
                SomeData itm = new SomeData();
                itm.Text = reader.ReadString();
                itm.Value = reader.ReadString();
                data.Add(itm);
            }
            

            return data;
            

        }
        public void sendReq()
        {
           NetworkStream netstream = socket.GetStream();

            BinaryWriter writer = new BinaryWriter(netstream);
           
            writer.Write(2);
              
            //writer.Close();
        }
       

    }
}
