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
    public class Certificate
    {
        public String siteName { set; get; }

        public String country { set; get; }
        public String city { set; get; }

        public String publicKey { set; get; }

    }
}
