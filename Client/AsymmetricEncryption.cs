using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Client
{
    public static class AsymmetricEncryption
    {
        private static bool _optimalAsymmetricEncryptionPadding = false;


        #region generate public and private keys
        public static RSACryptoServiceProvider GenerateKeys(int keySize)//, out string publicKey, out string publicAndPrivateKey)
        {
            return new RSACryptoServiceProvider(keySize);

        }
        #endregion

        #region PGP Method
        public static byte[] PGPEncrypt(string message, int keySize, string publicKeyXml)
        {

            //TODO
            byte[] data = Encoding.UTF8.GetBytes(message);
            SymmetricEncryption encryption = new SymmetricEncryption();

            byte[] encrypted = encryption.encrypt_data(data);

            byte[] key;

            key = Encrypt(encryption.key, 1024, publicKeyXml);

            byte[] msg = new byte[key.Length + encrypted.Length];

            Array.Copy(key, msg, key.Length);

            encrypted.CopyTo(msg, key.Length);

            return msg;

        }

        public static String PGPDecrypt(byte[] msg, string publicAndPrivateKey)
        {
            //TODO

            byte[] key = new byte[128];
            byte[] ms = new byte[msg.Length - 128];
            Array.Copy(msg, key, 128);

            byte[] dec = AsymmetricEncryption.Decrypt(key, 1024, publicAndPrivateKey);



            Array.Copy(msg, 128, ms, 0, ms.Length);

            SymmetricEncryption symmetric = new SymmetricEncryption();
            symmetric.key = dec;

            String res = symmetric.decrypt_data(ms);

            return res;
        }

        #endregion

        #region RSA Method
        public static byte[] Encrypt(byte[] data, int keySize, string publicKeyXml)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
            int maxLength = GetMaxDataLength(keySize);
            if (data.Length > maxLength)
                throw new ArgumentException(String.Format("Maximum data length is {0}", maxLength), "data");

            if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
            if (String.IsNullOrEmpty(publicKeyXml)) throw new ArgumentException("Key is null or empty", "publicKeyXml");

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicKeyXml);
                return provider.Encrypt(data, _optimalAsymmetricEncryptionPadding);
            }
        }



        public static byte[] Decrypt(byte[] data, int keySize, string publicAndPrivateKeyXml)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
            if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
            if (String.IsNullOrEmpty(publicAndPrivateKeyXml)) throw new ArgumentException("Key is null or empty", "publicAndPrivateKeyXml");

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicAndPrivateKeyXml);
                return provider.Decrypt(data, _optimalAsymmetricEncryptionPadding);
            }
        }

        public static int GetMaxDataLength(int keySize)
        {
            if (_optimalAsymmetricEncryptionPadding)
            {
                return ((keySize - 384) / 8) + 7;
            }
            return ((keySize - 384) / 8) + 37;
        }
        #endregion

        public static string EncryptText(string text, int keySize, string publicKeyXml)
        {
            var encrypted = Encrypt(Encoding.UTF8.GetBytes(text), keySize, publicKeyXml);
            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptText(string text, int keySize, string publicAndPrivateKeyXml)
        {
            var decrypted = Decrypt(Convert.FromBase64String(text), keySize, publicAndPrivateKeyXml);
            return Encoding.UTF8.GetString(decrypted);
        }

        public static bool IsKeySizeValid(int keySize)
        {
            return keySize >= 384 &&
                    keySize <= 16384 &&
                    keySize % 8 == 0;
        }
    }
}
