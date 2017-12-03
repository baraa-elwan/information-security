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
        public CA_info()
        {
            InitializeComponent();
        }

        private void btn_SendCA_Click(object sender, EventArgs e)
        {
            string path = @"D://"+tBox_SiteName.Text+".cer";
            string car = "Site Name : " + tBox_SiteName.Text + "\n"
                    + "Country : " + tBox_Country.Text + "\n" +
                    "City : " + tBox_City.Text;
            
            BinaryWriter writer;
            BinaryReader reader;

            TcpClient server_socket = new TcpClient();
            server_socket.Connect(System.Net.IPAddress.Parse("127.0.0.1"), 9999);
            writer = new BinaryWriter(server_socket.GetStream());
            //reader = new BinaryReader(server_socket.GetStream());
            //send request
            writer.Write("car");
            writer.Write(getBytes(car).Length);
            writer.Write(getBytes(car));
            writer.Close();

            //int len = reader.ReadInt32();
            //byte[] cer = reader.ReadBytes(len);

            //if (!File.Exists(path))
            //{
            //    File.WriteAllText(path, getString(cer));
            //}
            //else
            //{
            //    MessageBox.Show("the site have certifate");
            //}

            server_socket.Close();

            //MessageBox.Show("congratulation! \nyou get the certificate: " + getString(cer));
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
