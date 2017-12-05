using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Client
{
    [Serializable()]
    public class SomeData
    {
        public string username { set; get; }
        public Certificate info { set; get; }
        public byte[] certificate { get; set; }
    }
}
