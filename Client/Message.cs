using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
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


    }
}
