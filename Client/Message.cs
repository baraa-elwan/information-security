using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [Serializable()]
    public class Message 
    {
        public byte[] msg { get; set; }
        public byte[] signature { get; set; }
        public string file_name { get; set; }

        public Message(byte[] data, byte[] signature, string file_name){

            this.msg = data;
            this.signature = signature;
            this.file_name = file_name;


        }


        public static byte[] serilizeMessage(Message msg)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, msg);
           return stream.ToArray();
        }

        public static Message deSerilizeMessage(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return (Message)formatter.Deserialize(stream);
        }

    }
}
