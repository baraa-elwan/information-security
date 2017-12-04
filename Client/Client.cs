using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Net;
using System.Diagnostics;

namespace Client
{
    class Client
    {
        public TcpClient socket { get; set; }

        public string SIP;

        Process currentProcess = Process.GetCurrentProcess();

        public int SPort;

        public BinaryWriter writer;

        public Client()
        {
            socket = new TcpClient();

        }
       
        #region connect to server 
        public void connect(string host, int port)
        {
            //connect to server
            socket.Connect(
                System.Net.IPAddress.Parse(host),
                port
                );
            SIP = host;
            SPort = port;

            writer = new BinaryWriter(socket.GetStream());
        }
        #endregion

        public void disconnect()
        {
            socket.Close();
        }

        #region register client on the network
        public void sendUserData(string UName, string PublicK, string certificate)
        {

            try
            {
               
                writer.Write(1);
                writer.Write(PublicK);
                writer.Write(UName);
                writer.Write(certificate);
                writer.Flush();

            }
            catch
            {

            }
        }
        #endregion


        #region send "get client" request
        public void sendReq()
        {
        
            writer.Write(2);

            writer.Flush();
        }
        #endregion

        public void saveFile(String data, string path)
        {
            StreamWriter stream = new StreamWriter(path);

            stream.Write(data);

            stream.Close();
        }


        
    }
}
