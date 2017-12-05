using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class CA_info : Form
    {
        public Certificate certificate { set; get; }
        TcpClient client;
        string clientPublicKey { set; get; }
        ClientForm s;
        public string pk;
        public CA_info(string pk, ClientForm s)
        {
            this.s = s;
            clientPublicKey = pk;
            InitializeComponent();
        }

        private void btn_SendCA_Click(object sender, EventArgs e)
        {
            string path = @"D://" + tBox_SiteName.Text + ".cer";

            certificate = new Certificate()
            {
                siteName = tBox_SiteName.Text,
                country = tBox_Country.Text,
                city = tBox_City.Text,
                publicKey = clientPublicKey
            };

            client = new TcpClient();

            try
            {
                client.Connect(System.Net.IPAddress.Parse("127.0.0.1"), 9999);
                BinaryWriter writer = new BinaryWriter(client.GetStream());
                BinaryReader reader = new BinaryReader(client.GetStream());
                byte[] msg = Helper.Serilize(certificate);
                writer.Write(msg.Length);
                writer.Write(msg);
                writer.Flush();

                s.info = new SomeData();
                int l = reader.ReadInt32();

                s.info.certificate = reader.ReadBytes(l);
                s.CA = reader.ReadString();
            
                s.info.info = certificate;

            }
            catch
            {

            }

            this.Close();

        }

        public static byte[] getBytes(String str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static String getString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }
    }
}
