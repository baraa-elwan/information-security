using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Client
{
    public static class Helper
    {
        public static byte[] Serilize(Object obj)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            byte[] s = stream.ToArray();
            stream.Close();
            return s;
        }

        public static Object deSerilize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(stream);
        }

        public static byte[] getBytes(String str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static String getString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }


        public static string ShowDialogue(string directory)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.Filter = "All Files (*.txt)|*.txt";
            Dlg.CheckFileExists = true;
            Dlg.Title = "Choose a File";
            Dlg.InitialDirectory = @directory;
            if (Dlg.ShowDialog() == DialogResult.OK)
            {
                return Dlg.FileName;
            }
            return String.Empty;
        }
    }
}
