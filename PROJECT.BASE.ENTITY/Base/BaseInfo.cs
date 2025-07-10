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
    public partial class KeyValueInfo
    {
        [DataMember]
        [DisplayName("Key")]
        public string Key { get; set; }
        [DataMember]
        [DisplayName("Value")]
        public object Value { get; set; }

        public KeyValueInfo()
        {
            Key = string.Empty;     
            Value = null;            
		}
    }
}
