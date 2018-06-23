using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CodeStack.Community.DevTools.Sw.Testing.Parameters
{
    [DataContract]
    public class StartFromPathConnectionDetails : ConnectionDetails
    {
        [DataMember]
        public string ExecutablePath { get; set; }

        [DataMember]
        public int Timeout { get; set; } = 5;
    }
}
