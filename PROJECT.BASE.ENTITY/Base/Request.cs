using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class Request
    {
        [DataMember]
        [DisplayName("Key")]
        public string Key { get; set; }
        [DataMember]
        [DisplayName("Value")]
        public string Value { get; set; }

        [DataMember]
        [DisplayName("Sender")]
        public string Sender { get; set; }

        public Request()
        {
            Key = string.Empty;     
            Value = string.Empty;
            Sender = string.Empty;
		}
    }
}
