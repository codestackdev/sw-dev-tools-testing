using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CodeStack.Community.DevTools.Sw.Testing.Parameters
{
    [DataContract]
    [KnownType(typeof(StartFromPathConnectionDetails))]
    [KnownType(typeof(LoadEmbededConnectionDetails))]
    [KnownType(typeof(ConnectToProcessConnectionDetails))]
    public class TestServiceStartupParameters
    {
        [DataMember]
        public AppConnectOption_e ConnectOption { get; set; }

        [DataMember]
        public ConnectionDetails ConnectionDetails { get; set; }
    }
}
