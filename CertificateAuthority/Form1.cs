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
        SHA1Managed sha;
        BinaryReader reader;
        BinaryWriter writer;

        Thread thread;


        public Form1()
        {
            InitializeComponent();
            rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.ImportParameters(Program.privateKey);
            rsaProvider.ImportParameters(Program.publicKey);
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

            listener = new TcpListener(System.Net.IPAddress.Parse("127.0.0.1"), 9999);
            listener.Start();
            thread = new Thread(createCertificate);
            thread.Start();
        }


        public void createCertificate()
        {
            while (true)
            {
                if (listener.Pending())
                {
                    client = listener.AcceptTcpClient();
                    // NetworkStream stream = client.GetStream();
                    reader = new BinaryReader(client.GetStream());
                    writer = new BinaryWriter(client.GetStream());
                    reader.Close();
                    int len = reader.ReadInt32();
                    byte[] cer = reader.ReadBytes(len);


                    Certificate certificate = Certificate.deSerilizeMessage(cer);

                    DialogResult result = MessageBox.Show(
                          "authinticate " + certificate.name + "@" + certificate.company + ".com",
                          "new request",
                          MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        //byte[] cert = rsaProvider.SignHash(sha.ComputeHash(cer), CryptoConfig.MapNameToOID("SHA1"));
                        byte[] cert = rsaProvider.SignData(cer, new SHA1CryptoServiceProvider());
                        writer.Write(Client.Form1.getString(cert));
                        writer.Close();
                    }

                }

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }


    }
}
