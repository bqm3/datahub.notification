using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class DYNAMICInfo<T> where T : new()
    {
        [DataMember]
        [DisplayName("TOTAL_RECORD")]
        public long TOTAL_RECORD { get; set; }
        [DataMember]
        [DisplayName("DATA")]
        //public List<Dictionary<string, object>> DATA { get; set; }
        public List<T> DATA { get; set; }


    }
    
}
