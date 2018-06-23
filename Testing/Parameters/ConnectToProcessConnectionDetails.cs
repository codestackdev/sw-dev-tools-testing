using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CodeStack.Community.DevTools.Sw.Testing.Parameters
{
    [DataContract]
    public class ConnectToProcessConnectionDetails : ConnectionDetails
    {
        [DataMember]
        public int ProcessToConnect { get; set; }
    }
}
