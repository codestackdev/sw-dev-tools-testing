using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CodeStack.Community.DevTools.Sw.Testing.Parameters
{
    [DataContract]
    public enum AppConnectOption_e
    {
        [EnumMember]
        StartFromPath,

        [EnumMember]
        LoadEmbeded,

        [EnumMember]
        ConnectToProcess
    }
}
