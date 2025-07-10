using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class DATA_FIELDInfo
    {
        [DataMember]
        [DisplayName("FIELD_NAME")]
        public string FIELD_NAME { get; set; }
        [DataMember]
        [DisplayName("DATA_TYPE")]
        public string DATA_TYPE { get; set; }
        [DataMember]
        [DisplayName("FIELD_VALUE")]
        public object FIELD_VALUE { get; set; }

    }
}
