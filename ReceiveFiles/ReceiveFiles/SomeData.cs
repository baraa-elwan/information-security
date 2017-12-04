using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Server
{
    [Serializable()]
    public class SomeData
    {
        public string Text { get; set; }
        public string PK { get; set; }
        public string certificate { get; set; }
    }
}
