using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CertificateAuthority
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
       public static string publicAndprivateKey;
       public static RSAParameters pu;
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
                publicAndprivateKey = rsaProvider.ToXmlString(true);
                pu = rsaProvider.ExportParameters(true);
                Stream stream = new FileStream(path,FileMode.Create);
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, pu);

                stream.Close();
                flag = true;

            }
            else
            {
                Stream stream = new FileStream(path, FileMode.Open);
                IFormatter formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);
                pu = (RSAParameters)formatter.Deserialize(stream);
                flag = true;
                stream.Close();

            }


            if (flag)
                Application.Run(new CAForm());
            else
                MessageBox.Show("Error in generate key.");

            
            
        }
    }
}
