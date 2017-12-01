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
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string path = @"D://CertificateAuthorityPublicAndPrivateKey.txt";
            String publicAndPrivateKey;

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                int keySize = Convert.ToInt32(1024);
                RSACryptoServiceProvider rsaProvider = AsymmetricEncryption.GenerateKeys(keySize);
                publicAndPrivateKey = rsaProvider.ToXmlString(true);
                File.WriteAllText(path, publicAndPrivateKey);
            }
            else
            {
                publicAndPrivateKey = File.ReadAllText(path, Encoding.GetEncoding(20127));
            }

            if (publicAndPrivateKey != String.Empty)
                Application.Run(new Form1());
            else
                MessageBox.Show("Error in generate key.");

            
            
        }
    }
}
