using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.IO;
using Client;

namespace CertificateAuthority
{
    public partial class Form1 : Form
    {
        TcpListener listener;
        TcpClient client;
        RSACryptoServiceProvider rsaProvider;
        public Thread T = null;
        SHA1Managed sha;
        BinaryReader reader;
        BinaryWriter writer;
        public Form1(string publicKey)
        {
            InitializeComponent();
            rsaProvider = new RSACryptoServiceProvider();
            sha = new SHA1Managed();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //send public key to server
            TcpClient server_socket = new TcpClient();
            server_socket.Connect(System.Net.IPAddress.Parse("127.0.0.1"), 8888);
            writer = new BinaryWriter(server_socket.GetStream());
            writer.Write(99);
            writer.Write(rsaProvider.ToXmlString(false));
            writer.Close();
            server_socket.Close();

            ThreadStart Ts = new ThreadStart(ReceiveTCP);
            T = new Thread(Ts);
            T.Start();
        }

        public void ReceiveTCP()
        {
            try
            {
                listener = new TcpListener(System.Net.IPAddress.Parse("127.0.0.1"), 9999);
                listener.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            for (;;)
            {
                if (listener.Pending())
                {
                    client = new TcpClient();
                    client = listener.AcceptTcpClient();
                    Thread thread = new Thread(createCertificate);
                    thread.Start();

                }
            }
        }

        void createCertificate()
        {
            while (client.Connected)
            {
                //client = listener.AcceptTcpClient();
                // NetworkStream stream = client.GetStream();
                reader = new BinaryReader(client.GetStream());
                //writer = new BinaryWriter(client.GetStream());
                //reader.Close();
                string option = reader.ReadString();
                if (option == "car")
                {
                    int len = reader.ReadInt32();
                    byte[] cer = reader.ReadBytes(len);


                    Certificate certificate = Certificate.deSerilizeMessage(cer);

                    DialogResult result = MessageBox.Show(
                            "authinticate " + certificate.siteName + "." + certificate.country + "",
                            "new request",
                            MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        //byte[] cert = rsaProvider.SignHash(sha.ComputeHash(cer), CryptoConfig.MapNameToOID("SHA1"));
                        byte[] cert = rsaProvider.SignData(cer, new SHA1CryptoServiceProvider());
                        writer.Write(cert.Length);
                        writer.Write(cert);
                        writer.Close();
                    }
                }
            }

        }
    }
}
