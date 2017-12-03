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
   public class Certificate
    {
        public String siteName { set; get; }

        public String country { set; get; }
        public String city { set; get; }

        public String publicKey { set; get; }

       

        //public Certificate(string name, string company, string pKey)
        //{
        //    this.name = name;
        //    this.company = company;
        //    this.publicKey = pKey;
        //}

        public byte[] serilizeMessage()
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            return stream.ToArray();
        }

        public static Certificate deSerilizeMessage(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return (Certificate)formatter.Deserialize(stream);
        }


    }
}
