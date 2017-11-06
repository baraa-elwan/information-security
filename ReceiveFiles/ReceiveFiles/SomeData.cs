using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace ReceiveFiles
{
    [Serializable()]
    public class SomeData
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
