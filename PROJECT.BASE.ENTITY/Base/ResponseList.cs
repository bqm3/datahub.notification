using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PROJECT.BASE.ENTITY
{
    [Serializable]
    [DataContract]
    public partial class ResponseList<T> where T : new()
    {
        [DataMember]
        [DisplayName("ResultCode")]
        public string ResultCode { get; set; }

        [DataMember]
        [DisplayName("Message")]
        public string Message { get; set; }

        [DataMember]
        [DisplayName("TotalRecords")]
        public int TotalRecords { get; set; }

        [DataMember]
        [DisplayName("Data")]
        public List<T> Data { get; set; }

        public ResponseList()
        {
            ResultCode = string.Empty;           
            Message = string.Empty;
            TotalRecords = 0;
            Data = null;
		}
    }   
}
