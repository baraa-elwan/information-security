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
        string clientPublicKey;
        public CA_info(string PublicKey)
        {
            clientPublicKey = PublicKey;
            InitializeComponent();
        }

        private void btn_SendCA_Click(object sender, EventArgs e)
        {
            string path = @"D://"+tBox_SiteName.Text+".cer";
            ////string car = "Site Name : " + tBox_SiteName.Text + "\n"
            //        + "Country : " + tBox_Country.Text + "\n" +
            //        "City : " + tBox_City.Text;
            certificate = new Certificate()
            {
                siteName = tBox_SiteName.Text,
                country=tBox_Country.Text,
                city=tBox_City.Text,
                publicKey= clientPublicKey
            };
            this.Close();

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
