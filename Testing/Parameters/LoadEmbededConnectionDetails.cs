using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CodeStack.Community.DevTools.Sw.Testing.Parameters
{
    [DataContract]
    public class LoadEmbededConnectionDetails : ConnectionDetails
    {
        [DataMember]
        public bool UseAnyVersion { get; set; } = true;

        [DataMember]
        public int RevisionNumber { get; set; }
    }
}
