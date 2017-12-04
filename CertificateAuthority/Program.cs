using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace CertificateAuthority
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
        public static RSAParameters privateKey ;
        public static RSAParameters publicKey ;
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string path = @"D://CertificateAuthorityPublicAndPrivateKey.txt";
            bool flag;

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                int keySize = Convert.ToInt32(1024);
                RSACryptoServiceProvider rsaProvider = AsymmetricEncryption.GenerateKeys(keySize);
               // publicAndPrivateKey = rsaProvider.ToXmlString(true);
                publicKey = rsaProvider.ExportParameters(true);
                privateKey = rsaProvider.ExportParameters(false);
                BinaryWriter file = new BinaryWriter(new FileStream(path, FileMode.OpenOrCreate));
                file.Write(Client.Form1.getString(Helper.Serilize(publicKey)));
                file.Write(Client.Form1.getString(Helper.Serilize(privateKey)));
                flag = true;
            }
            else
            {
                BinaryReader file = new BinaryReader(new FileStream(path, FileMode.Open));
                privateKey = (RSAParameters)Helper.deSerilize(Client.Form1.getBytes(file.ReadString()));
                publicKey = (RSAParameters)Helper.deSerilize(Client.Form1.getBytes(file.ReadString()));
                flag = true;
            }


            if (flag)
                Application.Run(new Form1());
            else
                MessageBox.Show("Error in generate key.");

            
            
        }
    }
}
