using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
namespace Client
{
    class SymmetricEncryption
    {

        AesCryptoServiceProvider aes;

        public byte[] key
        {
            set { aes.Key = value; }
            get { return aes.Key; }
        }

       public void setKey(byte [] key)
        {
           this.key =(byte[]) key.Clone();
        }

        public SymmetricEncryption()
        {
            aes = new AesCryptoServiceProvider();
            aes.GenerateKey();
            aes.IV = System.Text.Encoding.UTF8.GetBytes("1234567891234567");
        }

        public byte[] encrypt_data(byte[] data)
        {
            byte[] encrypted;

            aes.Padding = PaddingMode.Zeros;
            aes.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            MemoryStream mem_stream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(mem_stream, encryptor, CryptoStreamMode.Write);

            StreamWriter st = new StreamWriter(cryptoStream);


            cryptoStream.Write(data, 0, data.Length);

            cryptoStream.FlushFinalBlock();

            encrypted = mem_stream.ToArray();

            return mem_stream.ToArray();

        }

        public String decrypt_data(byte[] data)
        {
            string plaintext = null;

            //class for aes algorithm

            aes.Padding = PaddingMode.Zeros;
            //
            aes.Mode = CipherMode.CBC;

            //خطوات تشفير 
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            MemoryStream mem_stream = new MemoryStream(data);

            CryptoStream cryptoStream = new CryptoStream(mem_stream, decryptor, CryptoStreamMode.Read);

            StreamReader st = new StreamReader(cryptoStream);

            plaintext = st.ReadToEnd();

            return plaintext;
        }

    }
}
